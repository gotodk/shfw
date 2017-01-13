using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.OracleClient;


public static class OracleHelper
{
    public static readonly string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
    public static readonly string ConnectionStringTest = ConfigurationManager.AppSettings["ConnectionStringTest"];
    public static readonly string connUseStatus = ConfigurationManager.AppSettings["connUse"];
    public static readonly string sqlHead = ConfigurationManager.AppSettings["sqlHead"];


    //Create a hashtable for the parameter cached
    private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

    /// <summary>
    /// Execute a database query which does not include a select
    /// </summary>
    /// <param name="connString">Connection string to database</param>
    /// <param name="cmdType">Command type either stored procedure or SQL</param>
    /// <param name="cmdText">Acutall SQL Command</param>
    /// <param name="commandParameters">Parameters to bind to the command</param>
    /// <returns></returns>
    public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    {
        // Create a new Oracle command
        OracleCommand cmd = new OracleCommand();

        //Create a connection
        using (OracleConnection connection = new OracleConnection(connectionString))
        {

            //Prepare the command
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

            //Execute the command
            int val = cmd.ExecuteNonQuery();
            connection.Close();
            cmd.Parameters.Clear();
            return val;
        }
    }


    /// <summary>
    /// 获取产品大类、产量大类、新产品项目
    /// </summary>
    /// <param name="fid"></param>
    /// <param name="materialClass">物料分类</param>
    /// <returns></returns>
    public static string GetClass(string fid, string materialClass)
    {
        string sql = "select FMaterialGroupID from " + sqlHead + "T_BD_MaterialGroupDetial where FMaterialID='" + fid + "' and FMaterialGroupStandardID='" + materialClass + "'";
        string UnitID = "";//计量单位             
        try
        {
            DataSet ds = Query(sql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UnitID = ds.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception sqlE)
        {

        }
        return UnitID;
    }

    //获取FID
    public static string GetFID(string type)
    {
        string sql = "SELECT " + sqlHead + "newbosid('" + type + "') as fid from dual";

        string fid = "";
        try
        {
            DataSet ds = Query(sql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    fid = ds.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception sqlE)
        {

        }
        return fid;
    }
    /// <summary>
    /// 根据登录员工获取部门
    /// </summary>
    /// <param name="fPersonID"></param>
    /// <returns></returns>
    public static string GetDepartmentByUser(string fuserID)
    {
        string sql = "SELECT t2.fadminorgunitid  from " + sqlHead + "T_ORG_PositionMember t1 " +
            "inner join " + sqlHead + "T_ORG_Position t2 on t1.fpositionid=t2.fid " +
                "inner join " + sqlHead + "t_pm_user t3 on t1.fpersonid=t3.fpersonid " +
            "where t3.fid='" + fuserID + "'";


        string UnitID = "";
        try
        {
            DataSet ds = Query(sql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UnitID = ds.Tables[0].Rows[0][0].ToString();
                }
            }
        }
        catch (Exception sqlE)
        {

        }

        return UnitID;
    }


    /// <summary>
    /// 根据部门获取成本中心ID
    /// </summary>
    /// <param name="fid"></param>
    /// <returns></returns>
    public static string GetCostCenterID(string fid)
    {
        string sql = "select * from " + sqlHead + "T_ORG_UnitRelation where ffromunitid='" + fid +
                "' and FTypeRelationID='00000000-0000-0000-0000-0000000000410FE9F8B5'";

        string UnitID = "";
        try
        {
            DataSet ds = Query(sql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    UnitID = ds.Tables[0].Rows[0]["ftounitid"].ToString();
                }
            }
        }
        catch (Exception sqlE)
        {

        }

        return UnitID;
    }

    /// <summary>
    /// 查询仓库信息
    /// </summary>
    /// <param name="fid"></param>
    /// <returns></returns>
    public static DataSet GetStocks()
    {
        string sql = @"Select t1.FID as FItemID,t1.FNumber,t1.FNAME_l2 as FName,t1.FwhmanID as FEmpID,t1.FDefaultLocationGroupID, 
        t1.FTransState,'' as Fname_l2 from " + sqlHead + "T_DB_WAREHOUSE t1 where t1.FWhState=1 and t1.FNAME_l2 like '四川成都库' and t1.FstorageOrgID='dE4AAAFoRWjM567U' " +
        " order by t1.FNumber";

        DataSet ds = null;
        try
        {
            ds = Query(sql);
        }
        catch (Exception sqlE)
        {

        }

        return ds;
    }

    /// <summary>
    /// 查询仓位信息
    /// </summary>
    /// <param name="fid"></param>
    /// <returns></returns>
    public static DataSet GetStockPlace(string stockid)
    {
        string sql = @"Select t1.fid as FSPID,t1.FNumber,t1.fname_l2 as FName,'' as FSPGroupID " +
            "from " + sqlHead + "T_DB_LOCATION t1 inner join " + sqlHead + "T_DB_WAREHOUSE t2 on t1.FWarehouseID=t2.fid " +
            "where t2.FWhState=1 and t2.FstorageOrgID='dE4AAAFoRWjM567U' ";// and t2.FstorageOrgID='dE4AAAFoRWjM567U'";";
        //string sql = @"Select t1.fid as FSPID,t1.FNumber,t1.fname_l2 as FName,'' as FSPGroupID "+
        //    "from T_DB_LOCATION t1 inner join T_DB_WAREHOUSE t2 on t1.FWarehouseID=t2.fid "+
        //    "where t2.FWhState=1 and t2.FstorageOrgID='dE4AAAFoRWjM567U' and t2.FWhState=1 ";// and t2.FstorageOrgID='dE4AAAFoRWjM567U'";";
        if (stockid != "")
        {
            sql += " and t2.fid='" + stockid + "'";
        }
        sql += " order by t1.fnumber ";
        DataSet ds = null;
        try
        {
            ds = Query(sql);
        }
        catch (Exception sqlE)
        {

        }

        return ds;
    }

    //查询客户
    public static DataTable selCustomer(string str)
    {
        string selCus = "select t.fid,t.fnumber,t.fname_l2 as fname,t.ftaxrate from  " + sqlHead + "t_bd_customer t where t.fusedstatus=1 and (t.fname_l2 like '%" + str + "%' or t.fnumber like '%" + str + "%') and rownum<=50 ";
        DataTable rDT = null;
        try
        {
            DataSet ds = Query(selCus);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rDT = ds.Tables[0];
                }
            }
        }
        catch (Exception sqlE)
        {

        }
        return rDT;
    }

    //查询出库、退货、借货单据
    public static DataTable selBills(string customerid, string sd, string ed, string sjxh)
    {
        //and (t1.fnumber like 'GKXOUT%') and t.FWarehouseID='dE4AAA0MPb276fiu'  --出库仓库为四川成都库  and t1.FBaseStatus>=4--单据状态为审核态
        string selSqlStr = @"select t1.fnumber as billNum,'销售出库' as billType,t7.fname_l2 as cusName,t1.fdescription,
        to_char(t1.fbizdate,'yyyy-mm-dd') as fbizdate,t.fseq,t4.fnumber,t4.fname_l2 as itemName,t4.fmodel,
        t.flot,t.fremark,t.fqty,t.FPrice,t.famount,t6.fnumber as RoutNumber,t5.fqty as RoutQty,
        (t.fqty+nvl(t5.fqty,0)) as xhQty  
        from wegoeas.T_IM_SaleIssueBill t1 inner join wegoeas.T_IM_SaleIssueEntry t on t.fparentid=t1.fid 
        left join wegoeas.T_BD_Material t4 on t4.fid=t.FMaterialID 
        left join wegoeas.T_IM_SaleIssueEntry t5 on t5.fsourcebillid=t1.fid and t5.fsourcebillentryid=t.fid 
        left join wegoeas.T_IM_SaleIssueBill t6 on t6.fid=t5.fparentid 
        left join wegoeas.t_bd_customer t7 on t7.fid=t1.FCustomerID 
        where t1.FStorageOrgUnitID='dE4AAAFoRWjM567U' and t1.FTransactionTypeID='0a7c476c-0106-1000-e000-01b8c0a812e6B008DCA7' and t.FWarehouseID='dE4AAA0MPb276fiu' and t1.FBaseStatus>=4 
        and t1.fbizdate>=to_timestamp('" + sd + "','yyyy-mm-dd hh:mi:ss') and t1.fbizdate<=to_timestamp('" + ed + "','yyyy-mm-dd hh:mi:ss')";

        if (customerid.Trim() != "")
        {
            selSqlStr += " and t7.fid='" + customerid + "'";
        }
        if (sjxh == "1")
        {
            selSqlStr += " and (t6.fnumber is null or (t.fqty+nvl(t5.fqty,0))>0) ";
        }
        selSqlStr += " order by t1.fbizdate,t1.fnumber,t.fseq";

        DataTable rDT = null;
        try
        {
            DataSet ds = Query(selSqlStr);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rDT = ds.Tables[0];
                }
            }
        }
        catch (Exception sqlE)
        {

        }
        return rDT;
    }

    //根据客户编码查询客户信息
    public static DataTable SelCustomer(string cusNum)
    {
        //FStorageOrgUnitID:XWA3gHiyS8GdY8OjkHVc5sznrtQ=
        //itemNum:11.02.03.001
        string selSqlStr = @"select t.fid,t.fnumber from t_bd_customer t where t.fnumber='" + cusNum + "'";

        DataTable rDT = null;
        try
        {
            DataSet ds = Query(selSqlStr);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rDT = ds.Tables[0];
                }
            }
        }
        catch (Exception sqlE)
        {

        }
        return rDT;
    }

    //查询物料信息
    public static DataTable SelItems(string itemNum,string FStorageOrgUnitID,string batchNo)
    {
        //FStorageOrgUnitID:XWA3gHiyS8GdY8OjkHVc5sznrtQ=
        //itemNum:11.02.03.001
        string selSqlStr = @"select t.fid as fitemid,t.fbaseunit as funitid,
        t2.FWarehouseID as stockID,t3.fname_l2 as stockName,t2.FLocationID as spID,t5.fname_l2 as spName,t2.flot   
        from T_BD_Material t left join T_IM_Inventory t2 on t2.FMaterialID=t.fid 
        and t2.FStorageOrgUnitID='" + FStorageOrgUnitID + @"' left join T_DB_WAREHOUSE t3 on t3.fid=t2.FWarehouseID 
        left join T_DB_LOCATION t5 on t5.fid=t2.FLocationID where t.fnumber like '" + itemNum + "' ";

        if (batchNo.Trim()!="")
        {
            selSqlStr += " and t2.flot='" + batchNo.Trim() + "'";
        }
        selSqlStr += " order by t2.flot asc";

        DataTable rDT = null;
        try
        {
            DataSet ds = Query(selSqlStr);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rDT = ds.Tables[0];
                }
            }
        }
        catch (Exception sqlE)
        {

        }
        return rDT;
    }

    //查询物料信息
    public static DataTable SelItems(string itemNum, string stockid, string cspid, string stockName, string cspName, string cusid)
    {
        string selSqlStr = @"select t.fid as fitemid,t.fnumber,t.fmodel,t.fname_l2 as itemname,'' as FBatchNO,
        '' as FBarCode, '" + stockName + @"' as StockID,nvl(t5.fname_l2,'') as StockPlaceID,(case max(nvl(t4.fprice,0)) when 0 then 0 else round(max(nvl(t4.fprice,0))/1.17,8) end) as price,
        '" + stockid + @"' as dcstockID,nvl(t5.fid,'') as cspID,'' as sourceentryid,'' as fseq,t.fbaseunit as funitid,
        0.17 as FTaxRate,max(nvl(t4.fprice,0)) as ftaxprice,(case max(nvl(t4.fprice,0)) when 0 then 0 else round(max(nvl(t4.fprice,0))/1.17,8) end) as Factualprice, 
        max(nvl(t4.fprice,0)) as factualtaxprice,'' as fpaymentcustomerid  
        from " + sqlHead + @"T_BD_Material t left join " + sqlHead + @"T_SCM_PricePolicyEntry t4 on t4.fmaterialid=t.fid 
        and t4.fcustomerid='" + cusid + "' left join " + sqlHead + @"T_IM_Inventory t2 on t2.FMaterialID=t.fid and t2.FCustomerID='" +
        cusid + "' and t2.FStorageOrgUnitID='dE4AAAFoRWjM567U' left join " + sqlHead + @"T_DB_WAREHOUSE t3 on t3.fid=t2.FWarehouseID and t3.fid='" +
        stockid + "' left join " + sqlHead + @"T_DB_LOCATION t5 on t5.fid=t2.FLocationID 
        where t.fnumber like '%" + itemNum + "%' and nvl(t5.fid,'')='" + cspid.Trim() + "' group by t.fid,t.fnumber,t.fmodel,t.fname_l2,t.fbaseunit ";

        DataTable rDT = null;
        try
        {
            DataSet ds = Query(selSqlStr);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rDT = ds.Tables[0];
                }
            }
        }
        catch (Exception sqlE)
        {

        }
        return rDT;
    }


    //查询是否在库，原函数注释掉了
    public static DataTable IsInStock(string barcode, string batch, string stockid, string cspid, string stockName, string cspName, string strSN, int selType, string cusid, ref string sql)
    {
        string selSqlStr = @"select t.fid as fitemid,t.fnumber,t.fmodel,t.fname_l2 as itemname, '" + batch + @"' as FBatchNO,
        t.FBarCode, '" + stockName + @"' as StockID,'" + cspName + @"' as StockPlaceID,(case max(nvl(t4.fprice,0)) when 0 then 0 else round(max(nvl(t4.fprice,0))/1.17,8) end) as price,
        '" + stockid + @"' as dcstockID,'" + cspid + @"' as cspID,'' as sourceentryid,'' as fseq,t.fbaseunit as funitid,
        0.17 as FTaxRate,max(nvl(t4.fprice,0)) as ftaxprice,(case max(nvl(t4.fprice,0)) when 0 then 0 else round(max(nvl(t4.fprice,0))/1.17,8) end) as Factualprice, 
        max(nvl(t4.fprice,0)) as factualtaxprice,'' as fpaymentcustomerid  
        from  " +
        sqlHead + @"T_BD_Material t  left join " +
        sqlHead + @"T_SCM_PricePolicyEntry t4 on t4.fmaterialid=t.fid and t4.fcustomerid='" + cusid +
        "' where t.FBarCode='" + barcode + "' " + "" + @"  
        group by t.fid,t.fnumber,t.fmodel,t.fname_l2,t.fbaseunit,t.FBarCode ";
        sql = selSqlStr;
        DataTable rDT = null;
        try
        {
            DataSet ds = Query(selSqlStr);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rDT = ds.Tables[0];
                }
            }
        }
        catch (Exception sqlE)
        {
            throw sqlE;
        }
        return rDT;
    }


    //        //查询是否在库
    //        public static DataTable IsInStock(string barcode,string batch,string stockid,string cspid,string stockName ,string cspName,string strSN,int selType,string cusid)
    //        {
    //            string selSqlStr = @"select t1.fitemid,t.fnumber,t.fmodel,t.fname_l2 as itemname,t1.FBatchNO,
    //            t1.FBarCode, '" + stockName + @"' as StockID,'" + cspName + @"' as StockPlaceID,(case max(nvl(t4.fprice,0)) when 0 then 0 else round(max(nvl(t4.fprice,0))/1.17,8) end) as price,
    //            '" + stockid + @"' as dcstockID,'" + cspid + @"' as cspID,'' as sourceentryid,'' as fseq,t.fbaseunit as funitid,
    //            0.17 as FTaxRate,max(nvl(t4.fprice,0)) as ftaxprice,(case max(nvl(t4.fprice,0)) when 0 then 0 else round(max(nvl(t4.fprice,0))/1.17,8) end) as Factualprice, 
    //            max(nvl(t4.fprice,0)) as factualtaxprice,'' as fpaymentcustomerid  
    //            from ZCT_ICSerial t1 inner join " + sqlHead + @"T_BD_Material t on t.fid=t1.fitemid left join " +
    //            sqlHead + @"T_SCM_PricePolicyEntry t4 on t4.fmaterialid=t1.fitemid and t4.fcustomerid='" + cusid + 
    //            "' where t1.FBarCode='" + barcode +"' and t1.FBatchNO = '" + batch + @"' and t1.fserialnum='" + strSN + @"'  
    //            group by t1.fitemid,t.fnumber,t.fmodel,t.fname_l2,t1.FBatchNO,t1.FBarCode,t.fbaseunit ";

    //            DataTable rDT = null;
    //            try
    //            {
    //                DataSet ds = Query(selSqlStr);
    //                if (ds != null)
    //                {
    //                    if (ds.Tables[0].Rows.Count > 0)
    //                    {
    //                        rDT = ds.Tables[0];
    //                    }
    //                }
    //            }
    //            catch (Exception sqlE)
    //            {

    //            }
    //            return rDT;
    //        }


    /// <summary>
    /// 执行查询语句，返回DataSet
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataSet</returns>
    public static DataSet Query(string SQLString)
    {
        string connStr = connectionString;
        if (connUseStatus == "0")
        {
            connStr = ConnectionStringTest;
        }
        using (OracleConnection connection = new OracleConnection(connStr))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");
            }
            catch (OracleException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            return ds;
        }
    }

    public static DataSet Query(string SQLString, params OracleParameter[] cmdParms)
    {
        string connStr = connectionString;
        if (connUseStatus == "0")
        {
            connStr = ConnectionStringTest;
        }
        using (OracleConnection connection = new OracleConnection(connStr))
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, null, SQLString, cmdParms);
            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "ds");
                    cmd.Parameters.Clear();
                }
                catch (System.Data.OracleClient.OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
                return ds;
            }
        }
    }

    private static void PrepareCommand(OracleCommand cmd, OracleTransaction trans, string cmdText, OracleParameter[] cmdParms)
    {
        using (OracleConnection conn = new OracleConnection(connectionString))
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OracleParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
    }

    /// <summary>
    /// 执行一条计算查询结果语句，返回查询结果（object）。
    /// </summary>
    /// <param name="SQLString">计算查询结果语句</param>
    /// <returns>查询结果（object）</returns>
    public static object GetSingle(string SQLString)
    {
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            using (OracleCommand cmd = new OracleCommand(SQLString, connection))
            {
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }

    public static bool Exists(string strOracle)
    {
        object obj = OracleHelper.GetSingle(strOracle);
        int cmdresult;
        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
        {
            cmdresult = 0;
        }
        else
        {
            cmdresult = int.Parse(obj.ToString());
        }
        if (cmdresult == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Execute an OracleCommand (that returns no resultset) against an existing database transaction 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    /// </remarks>
    /// <param name="trans">an existing database transaction</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    {
        OracleCommand cmd = new OracleCommand();
        PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
        int val = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        return val;
    }

    /// <summary>
    /// Execute an OracleCommand (that returns no resultset) against an existing database connection 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    /// </remarks>
    /// <param name="conn">an existing database connection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    {

        OracleCommand cmd = new OracleCommand();

        PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
        int val = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        return val;
    }
    /// <summary>
    /// Execute an OracleCommand (that returns no resultset) against an existing database connection 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    /// </remarks>
    /// <param name="conn">an existing database connection</param>
    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(string cmdText)
    {

        OracleCommand cmd = new OracleCommand();
        OracleConnection connection = new OracleConnection(connectionString);
        PrepareCommand(cmd, connection, null, CommandType.Text, cmdText, null);
        int val = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        return val;
    }

    /// <summary>
    /// Execute a select query that will return a result set
    /// </summary>
    /// <param name="connString">Connection string</param>
    //// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    /// <returns></returns>
    public static OracleDataReader ExecuteReader(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    {
        OracleCommand cmd = new OracleCommand();
        OracleConnection conn = new OracleConnection(connectionString);
        try
        {
            //Prepare the command to execute
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
        }
        catch
        {
            conn.Close();
            throw;
        }
    }

    /// <summary>
    /// Execute an OracleCommand that returns the first column of the first record against the database specified in the connection string 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    /// </remarks>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
    public static object ExecuteScalar(CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    {
        OracleCommand cmd = new OracleCommand();

        using (OracleConnection conn = new OracleConnection(connectionString))
        {
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
    }

    ///	<summary>
    ///	Execute	a OracleCommand (that returns a 1x1 resultset)	against	the	specified SqlTransaction
    ///	using the provided parameters.
    ///	</summary>
    ///	<param name="transaction">A	valid SqlTransaction</param>
    ///	<param name="commandType">The CommandType (stored procedure, text, etc.)</param>
    ///	<param name="commandText">The stored procedure name	or PL/SQL command</param>
    ///	<param name="commandParameters">An array of	OracleParamters used to execute the command</param>
    ///	<returns>An	object containing the value	in the 1x1 resultset generated by the command</returns>
    public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
    {
        if (transaction == null)
            throw new ArgumentNullException("transaction");
        if (transaction != null && transaction.Connection == null)
            throw new ArgumentException("The transaction was rollbacked	or commited, please	provide	an open	transaction.", "transaction");

        // Create a	command	and	prepare	it for execution
        OracleCommand cmd = new OracleCommand();

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

        // Execute the command & return	the	results
        object retval = cmd.ExecuteScalar();

        // Detach the SqlParameters	from the command object, so	they can be	used again
        cmd.Parameters.Clear();
        return retval;
    }

    /// <summary>
    /// Execute an OracleCommand that returns the first column of the first record against an existing database connection 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  Object obj = ExecuteScalar(conn, CommandType.StoredProcedure, "PublishOrders", new OracleParameter(":prodid", 24));
    /// </remarks>
    /// <param name="conn">an existing database connection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or PL/SQL command</param>
    /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
    /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
    public static object ExecuteScalar(OracleConnection connectionString, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
    {
        OracleCommand cmd = new OracleCommand();

        PrepareCommand(cmd, connectionString, null, cmdType, cmdText, commandParameters);
        object val = cmd.ExecuteScalar();
        cmd.Parameters.Clear();
        return val;
    }

    /// <summary>
    /// Add a set of parameters to the cached
    /// </summary>
    /// <param name="cacheKey">Key value to look up the parameters</param>
    /// <param name="commandParameters">Actual parameters to cached</param>
    public static void CacheParameters(string cacheKey, params OracleParameter[] commandParameters)
    {
        parmCache[cacheKey] = commandParameters;
    }

    /// <summary>
    /// Fetch parameters from the cache
    /// </summary>
    /// <param name="cacheKey">Key to look up the parameters</param>
    /// <returns></returns>
    public static OracleParameter[] GetCachedParameters(string cacheKey)
    {
        OracleParameter[] cachedParms = (OracleParameter[])parmCache[cacheKey];

        if (cachedParms == null)
            return null;

        // If the parameters are in the cache
        OracleParameter[] clonedParms = new OracleParameter[cachedParms.Length];

        // return a copy of the parameters
        for (int i = 0, j = cachedParms.Length; i < j; i++)
            clonedParms[i] = (OracleParameter)((ICloneable)cachedParms[i]).Clone();

        return clonedParms;
    }
    /// <summary>
    /// Internal function to prepare a command for execution by the database
    /// </summary>
    /// <param name="cmd">Existing command object</param>
    /// <param name="conn">Database connection object</param>
    /// <param name="trans">Optional transaction object</param>
    /// <param name="cmdType">Command type, e.g. stored procedure</param>
    /// <param name="cmdText">Command test</param>
    /// <param name="commandParameters">Parameters for the command</param>
    private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
    {

        //Open the connection if required
        if (conn.State != ConnectionState.Open)
            conn.Open();

        //Set up the command
        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        cmd.CommandType = cmdType;

        //Bind it to the transaction if it exists
        if (trans != null)
            cmd.Transaction = trans;

        // Bind the parameters passed in
        if (commandParameters != null)
        {
            foreach (OracleParameter parm in commandParameters)
                cmd.Parameters.Add(parm);
        }
    }

    /// <summary>
    /// Converter to use boolean data type with Oracle
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <returns></returns>
    public static string OraBit(bool value)
    {
        if (value)
            return "Y";
        else
            return "N";
    }

    /// <summary>
    /// Converter to use boolean data type with Oracle
    /// </summary>
    /// <param name="value">Value to convert</param>
    /// <returns></returns>
    public static bool OraBool(string value)
    {
        if (value.Equals("Y"))
            return true;
        else
            return false;
    }

    /// <summary>
    /// 执行多条SQL语句，实现数据库事务。
    /// </summary>
    /// <param name="SQLStringList">多条SQL语句</param>		
    public static void ExecuteSqlTran(List<String> SQLStringList)
    {
        string connStr = connectionString;
        if (connUseStatus == "0")
        {
            connStr = ConnectionStringTest;
        }
        using (OracleConnection conn = new OracleConnection(connStr))
        {
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            OracleTransaction tx = conn.BeginTransaction();
            cmd.Transaction = tx;
            try
            {
                foreach (string sql in SQLStringList)
                {
                    if (!String.IsNullOrEmpty(sql))
                    {
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
                tx.Commit();
            }
            catch (System.Data.OracleClient.OracleException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
    }
}