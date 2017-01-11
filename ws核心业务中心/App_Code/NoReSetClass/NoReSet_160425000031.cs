using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;
using System.Web.Script.Serialization;

public class NoReSet_160425000031
{
 

    /// <summary>
    /// ��ʼ������ֵ���ݼ�,ִ�н��ֻ������ok��err(���������������׼)
    /// </summary>
    /// <returns></returns>
    private DataSet initReturnDataSet()
    {
        DataSet ds = new DataSet();
        DataTable auto2 = new DataTable();
        auto2.TableName = "����ֵ����";
        auto2.Columns.Add("ִ�н��");
        auto2.Columns.Add("��ʾ�ı�");
        auto2.Columns.Add("������Ϣ1");
        auto2.Columns.Add("������Ϣ2");
        auto2.Columns.Add("������Ϣ3");
        auto2.Columns.Add("������Ϣ4");
        auto2.Columns.Add("������Ϣ5");
        ds.Tables.Add(auto2);
        return ds;
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="parameter_forUI">ǰ̨�������Ĳ���</param>
    /// <returns></returns>
    public DataSet NRS_ADD(DataTable parameter_forUI)
    {
        //����ת������
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["������"].ToString()] = parameter_forUI.Rows[i]["����ֵ"].ToString();
        }
        //��ʼ������ֵ
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["����ֵ����"].Rows.Add(new string[] { "err", "��ʼ��" });
        //�����Ϸ��Ը�����֤������Ҫ���ݾ���ҵ���߼�����

        //��ʼ�����Ĵ�������ҵ���߼��������ݿ�
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        //�Կ�����guid��ʽ����
        //SID, S_YYID, Skeshi, Sleixing, Sbanben, Schuchangriqi, Sanzhuangriqi, Sbaoxiudaoqi, Schenbenjia,   Sbaoxiuqixian, Sbaoyangzhouqi, Szhuangtai, Scaigouqudao, Sdailishang,   Sshouming, Sxiaoshoujiage
        param.Add("@SID", ht_forUI["SID"].ToString().Trim());
        param.Add("@S_YYID", ht_forUI["S_YYID"].ToString());
        param.Add("@Skeshi", ht_forUI["Skeshi"].ToString());
        param.Add("@S_SBID", ht_forUI["S_SBID"].ToString());
        param.Add("@Smingcheng", ht_forUI["Smingcheng"].ToString());
        param.Add("@Sxinghao", ht_forUI["Sxinghao"].ToString());

        param.Add("@Sbanben", ht_forUI["Sbanben"].ToString());
        param.Add("@Schuchangriqi", ht_forUI["Schuchangriqi"].ToString());
        param.Add("@Sanzhuangriqi", ht_forUI["Sanzhuangriqi"].ToString());
        param.Add("@Sbaoxiudaoqi", ht_forUI["Sbaoxiudaoqi"].ToString());
        param.Add("@Sbaoyangdaoqi", ht_forUI["Sbaoyangdaoqi"].ToString());
        param.Add("@Schenbenjia", ht_forUI["Schenbenjia"].ToString());
        param.Add("@Sbaoxiuqixian", ht_forUI["Sbaoxiuqixian"].ToString());
        param.Add("@Sbaoyangzhouqi", ht_forUI["Sbaoyangzhouqi"].ToString());
        param.Add("@Szhuangtai", ht_forUI["Szhuangtai"].ToString());
        param.Add("@Scaigouqudao", ht_forUI["Scaigouqudao"].ToString());
        param.Add("@Sdailishang", ht_forUI["Sdailishang"].ToString());
        param.Add("@Sshouming", ht_forUI["Sshouming"].ToString());
        param.Add("@Sxiaoshoujiage", ht_forUI["Sxiaoshoujiage"].ToString());

        param.Add("@Ssfby", ht_forUI["Ssfby"].ToString());

        if (ht_forUI.Contains("allpath_file1"))
        { param.Add("@Sfujian", ht_forUI["allpath_file1"].ToString()); }
        else
        {
            param.Add("@Sfujian", "");
        }

        alsql.Add("INSERT INTO   ZZZ_WFSB(SID, S_YYID, Skeshi, S_SBID,Smingcheng,Sxinghao, Sbanben, Schuchangriqi, Sanzhuangriqi, Sbaoxiudaoqi,Sbaoyangdaoqi, Schenbenjia,   Sbaoxiuqixian, Sbaoyangzhouqi, Szhuangtai, Scaigouqudao, Sdailishang,   Sshouming, Sxiaoshoujiage,Sfujian,Ssfby ) VALUES(@SID, @S_YYID, @Skeshi, @S_SBID,@Smingcheng,@Sxinghao, @Sbanben, @Schuchangriqi, @Sanzhuangriqi, @Sbaoxiudaoqi,@Sbaoyangdaoqi, @Schenbenjia,   @Sbaoxiuqixian, @Sbaoyangzhouqi, @Szhuangtai, @Scaigouqudao, @Sdailishang,   @Sshouming, @Sxiaoshoujiage,@Sfujian,@Ssfby)");



        //�����ӱ� ���� (�����Ϣ)
        string zibiao_lj_id = "grid-table-subtable-160726000004";
        DataTable subdt_lj = jsontodatatable.ToDataTable(ht_forUI[zibiao_lj_id].ToString());
        //������֤js�ű���ȡ��������c#�����л���ȡ������һ�²��ܼ�������ֹ����
        if (ht_forUI[zibiao_lj_id + "_fcjsq"].ToString() != subdt_lj.Rows.Count.ToString())
        {
            dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "err";
            dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "�ӱ����������ȡ���������ϵͳ�������⡣";
            return dsreturn;
        }

        param.Add("@sub_" + "MainID", ht_forUI["SID"].ToString()); //��������id

        for (int i = 0; i < subdt_lj.Rows.Count; i++)
        {
            param.Add("@sub_" + "ljid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_WFSB_LJ"));

            param.Add("@sub_" + "lj_LID" + "_" + i, subdt_lj.Rows[i]["������"].ToString());
            param.Add("@sub_" + "ljmingcheng" + "_" + i, subdt_lj.Rows[i]["�������"].ToString());
            param.Add("@sub_" + "ljxinghao" + "_" + i, subdt_lj.Rows[i]["����ͺ�"].ToString());
            param.Add("@sub_" + "ljdanwei" + "_" + i, subdt_lj.Rows[i]["�����λ"].ToString());
            param.Add("@sub_" + "ljweizhi" + "_" + i, subdt_lj.Rows[i]["λ�ñ��"].ToString());
            param.Add("@sub_" + "ljsjsj" + "_" + i, subdt_lj.Rows[i]["ʵ���ۼ�"].ToString());
            param.Add("@sub_" + "ljlsj" + "_" + i, subdt_lj.Rows[i]["���ۼ�"].ToString());
            param.Add("@sub_" + "ljshuliang" + "_" + i, subdt_lj.Rows[i]["�������"].ToString());
            param.Add("@sub_" + "ljzje" + "_" + i, subdt_lj.Rows[i]["���"].ToString());
            param.Add("@sub_" + "ljbaoxiujiezhi" + "_" + i, subdt_lj.Rows[i]["���޽�ֹ����"].ToString());
            param.Add("@sub_" + "ljpihao" + "_" + i, subdt_lj.Rows[i]["����"].ToString());
            param.Add("@sub_" + "ljbeizhu" + "_" + i, subdt_lj.Rows[i]["��ע"].ToString());
 

            string INSERTsql = "INSERT INTO ZZZ_WFSB_LJ (  ljid, lj_SBID, lj_LID, ljmingcheng, ljxinghao, ljdanwei,ljweizhi, ljsjsj, ljlsj, ljshuliang, ljzje, ljbaoxiujiezhi, ljpihao,   ljbeizhu) VALUES(@sub_" + "ljid" + "_" + i + ", @sub_MainID, @sub_" + "lj_LID" + "_" + i + ", @sub_" + "ljmingcheng" + "_" + i + ", @sub_" + "ljxinghao" + "_" + i + ", @sub_" + "ljdanwei" + "_" + i + ",@sub_" + "ljweizhi" + "_" + i + ", @sub_" + "ljsjsj" + "_" + i + ", @sub_" + "ljlsj" + "_" + i + ", @sub_" + "ljshuliang" + "_" + i + ", @sub_" + "ljzje" + "_" + i + ", @sub_" + "ljbaoxiujiezhi" + "_" + i + ", @sub_" + "ljpihao" + "_" + i + ", @sub_" + "ljbeizhu" + "_" + i + " )";
            alsql.Add(INSERTsql);
        }



        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "ok";
            dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "�����ɹ���{" + ht_forUI["SID"].ToString() + "}";
        }
        else
        {
            dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "err";
            dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "ϵͳ���ϣ�����ʧ�ܣ�" + return_ht["return_errmsg"].ToString();
        }
        return dsreturn;
    }

    /// <summary>
    /// �༭����
    /// </summary>
    /// <param name="parameter_forUI">ǰ̨�������Ĳ���</param>
    /// <returns></returns>
    public DataSet NRS_EDIT(DataTable parameter_forUI)
    {
        //����ת������
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["������"].ToString()] = parameter_forUI.Rows[i]["����ֵ"].ToString();
        }


        //��ʼ������ֵ
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["����ֵ����"].Rows.Add(new string[] { "err", "��ʼ��" });

        //�����Ϸ��Ը�����֤������ʡ��
        if (ht_forUI["idforedit"].ToString().Trim() == "")
        {
            dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "err";
            dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "û����ȷ���޸�Ŀ�꣡";
            return dsreturn;
        }
        //��ʼ�����Ĵ�������ֻ����ʾ������ֱ��������дҵ���߼�������

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();

        ht_forUI["idforedit"] = System.Web.HttpUtility.UrlDecode(ht_forUI["idforedit"].ToString());

        param.Add("@SID", ht_forUI["idforedit"].ToString());


        param.Add("@S_YYID", ht_forUI["S_YYID"].ToString());
        param.Add("@Skeshi", ht_forUI["Skeshi"].ToString());
        param.Add("@S_SBID", ht_forUI["S_SBID"].ToString());
        param.Add("@Smingcheng", ht_forUI["Smingcheng"].ToString());
        param.Add("@Sxinghao", ht_forUI["Sxinghao"].ToString());

        param.Add("@Sbanben", ht_forUI["Sbanben"].ToString());
        param.Add("@Schuchangriqi", ht_forUI["Schuchangriqi"].ToString());
        param.Add("@Sanzhuangriqi", ht_forUI["Sanzhuangriqi"].ToString());
        param.Add("@Sbaoxiudaoqi", ht_forUI["Sbaoxiudaoqi"].ToString());
        param.Add("@Sbaoyangdaoqi", ht_forUI["Sbaoyangdaoqi"].ToString());
        param.Add("@Schenbenjia", ht_forUI["Schenbenjia"].ToString());
        param.Add("@Sbaoxiuqixian", ht_forUI["Sbaoxiuqixian"].ToString());
        param.Add("@Sbaoyangzhouqi", ht_forUI["Sbaoyangzhouqi"].ToString());
        param.Add("@Szhuangtai", ht_forUI["Szhuangtai"].ToString());
        param.Add("@Scaigouqudao", ht_forUI["Scaigouqudao"].ToString());
        param.Add("@Sdailishang", ht_forUI["Sdailishang"].ToString());
        param.Add("@Sshouming", ht_forUI["Sshouming"].ToString());
        param.Add("@Sxiaoshoujiage", ht_forUI["Sxiaoshoujiage"].ToString());

        param.Add("@Ssfby", ht_forUI["Ssfby"].ToString());

        if (ht_forUI.Contains("allpath_file1"))
        { param.Add("@Sfujian", ht_forUI["allpath_file1"].ToString()); }
        else
        {
            param.Add("@Sfujian", "");
        }

        alsql.Add("UPDATE ZZZ_WFSB SET S_YYID=@S_YYID, Skeshi=@Skeshi, S_SBID=@S_SBID,Smingcheng=@Smingcheng,Sxinghao=@Sxinghao, Sbanben=@Sbanben, Schuchangriqi=@Schuchangriqi, Sanzhuangriqi=@Sanzhuangriqi, Sbaoxiudaoqi=@Sbaoxiudaoqi,Sbaoyangdaoqi=@Sbaoyangdaoqi, Schenbenjia=@Schenbenjia,   Sbaoxiuqixian=@Sbaoxiuqixian, Sbaoyangzhouqi=@Sbaoyangzhouqi, Szhuangtai=@Szhuangtai, Scaigouqudao=@Scaigouqudao, Sdailishang=@Sdailishang,   Sshouming=@Sshouming, Sxiaoshoujiage=@Sxiaoshoujiage ,Sfujian=@Sfujian,Ssfby=@Ssfby where SID=@SID ");

        //��¼���к��޸ļ�¼
     
        if (ht_forUI["idforedit"].ToString().Trim() != ht_forUI["SID"].ToString().Trim())
        {
            string guidhh = CombGuid.GetMewIdFormSequence("ZZZ_WFSB_xlh_his");
            param.Add("@id", guidhh);
            param.Add("@new_SID", ht_forUI["SID"].ToString());
            param.Add("@old_SID", ht_forUI["idforedit"].ToString());
            param.Add("@xgr", ht_forUI["yhbsp_session_uer_UAid"].ToString());
            alsql.Add("INSERT INTO ZZZ_WFSB_xlh_his (id,old_SID,new_SID,xgr) VALUES (@id,@old_SID,@new_SID,@xgr) ");
            alsql.Add("update ZZZ_WFSB  set SID=@new_SID where SID=@old_SID");
            alsql.Add("update ZZZ_WFSB_LJ  set lj_SBID=@new_SID where lj_SBID=@old_SID");
            
        }

        //��¼�ͻ��޸�
        if (ht_forUI["old_S_YYID"].ToString().Trim() != ht_forUI["S_YYID"].ToString().Trim())
        {
            string guidhh = CombGuid.GetMewIdFormSequence("ZZZ_WFSB_xlh_his");
            param.Add("@SID_0", ht_forUI["SID"].ToString().Trim());
            param.Add("@id_0", guidhh);
            param.Add("@xgr", ht_forUI["yhbsp_session_uer_UAid"].ToString());
            param.Add("@xgmsg", "�ͻ���"+ ht_forUI["old_S_YYID"].ToString() + "���Ϊ"+ ht_forUI["S_YYID"].ToString());
            alsql.Add("INSERT INTO ZZZ_WFSB_xlh_his (id,old_SID,new_SID,xgr,xgmsg) VALUES (@id_0,@SID_0,@SID_0,@xgr,@xgmsg) ");
 

        }

        //δ�޸����к�ʱ���ű����ӱ�
        if (ht_forUI["idforedit"].ToString().Trim() == ht_forUI["SID"].ToString().Trim())
        {
            //�����ӱ� ���� (�����Ϣ)
            string zibiao_lj_id = "grid-table-subtable-160726000004";
            DataTable subdt_lj = jsontodatatable.ToDataTable(ht_forUI[zibiao_lj_id].ToString());
            //������֤js�ű���ȡ��������c#�����л���ȡ������һ�²��ܼ�������ֹ����
            if (ht_forUI[zibiao_lj_id + "_fcjsq"].ToString() != subdt_lj.Rows.Count.ToString())
            {
                dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "err";
                dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "�ӱ����������ȡ���������ϵͳ�������⡣";
                return dsreturn;
            }


            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //��������id
            alsql.Add("delete ZZZ_WFSB_LJ where  lj_SBID = @sub_" + "MainID");
            for (int i = 0; i < subdt_lj.Rows.Count; i++)
            {

                if (subdt_lj.Rows[i]["���ر��"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "ljid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_WFSB_LJ"));
                }
                else
                {
                    param.Add("@sub_" + "ljid" + "_" + i, subdt_lj.Rows[i]["���ر��"].ToString());
                }

                param.Add("@sub_" + "lj_LID" + "_" + i, subdt_lj.Rows[i]["������"].ToString());
                param.Add("@sub_" + "ljmingcheng" + "_" + i, subdt_lj.Rows[i]["�������"].ToString());
                param.Add("@sub_" + "ljxinghao" + "_" + i, subdt_lj.Rows[i]["����ͺ�"].ToString());
                param.Add("@sub_" + "ljdanwei" + "_" + i, subdt_lj.Rows[i]["�����λ"].ToString());
                param.Add("@sub_" + "ljweizhi" + "_" + i, subdt_lj.Rows[i]["λ�ñ��"].ToString());
                param.Add("@sub_" + "ljsjsj" + "_" + i, subdt_lj.Rows[i]["ʵ���ۼ�"].ToString());
                param.Add("@sub_" + "ljlsj" + "_" + i, subdt_lj.Rows[i]["���ۼ�"].ToString());
                param.Add("@sub_" + "ljshuliang" + "_" + i, subdt_lj.Rows[i]["�������"].ToString());
                param.Add("@sub_" + "ljzje" + "_" + i, subdt_lj.Rows[i]["���"].ToString());
                param.Add("@sub_" + "ljbaoxiujiezhi" + "_" + i, subdt_lj.Rows[i]["���޽�ֹ����"].ToString());
                param.Add("@sub_" + "ljpihao" + "_" + i, subdt_lj.Rows[i]["����"].ToString());
                param.Add("@sub_" + "ljbeizhu" + "_" + i, subdt_lj.Rows[i]["��ע"].ToString());


                string INSERTsql = "INSERT INTO ZZZ_WFSB_LJ (  ljid, lj_SBID, lj_LID, ljmingcheng, ljxinghao, ljdanwei,ljweizhi, ljsjsj, ljlsj, ljshuliang, ljzje, ljbaoxiujiezhi, ljpihao,   ljbeizhu) VALUES(@sub_" + "ljid" + "_" + i + ", @sub_MainID, @sub_" + "lj_LID" + "_" + i + ", @sub_" + "ljmingcheng" + "_" + i + ", @sub_" + "ljxinghao" + "_" + i + ", @sub_" + "ljdanwei" + "_" + i + ",@sub_" + "ljweizhi" + "_" + i + ", @sub_" + "ljsjsj" + "_" + i + ", @sub_" + "ljlsj" + "_" + i + ", @sub_" + "ljshuliang" + "_" + i + ", @sub_" + "ljzje" + "_" + i + ", @sub_" + "ljbaoxiujiezhi" + "_" + i + ", @sub_" + "ljpihao" + "_" + i + ", @sub_" + "ljbeizhu" + "_" + i + " )";
                alsql.Add(INSERTsql);
            }
        }





        return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "ok";
            dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "�޸ĳɹ���{" + ht_forUI["SID"].ToString() + "}";
        }
        else
        {
            //��ʵҪ��¼��־�����������������ֻ����ʾ
            //dsreturn.Tables.Add(parameter_forUI.Copy());
            dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "err";
            dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "ϵͳ���ϣ��޸�ʧ�ܣ�" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }

    /// <summary>
    /// �༭����ǰ��ȡ����
    /// </summary>
    /// <param name="parameter_forUI">ǰ̨�������Ĳ���</param>
    /// <returns></returns>
    public DataSet NRS_EDIT_INFO(DataTable parameter_forUI)
    {
        //����ת������
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["������"].ToString()] = parameter_forUI.Rows[i]["����ֵ"].ToString();
        }


        //��ʼ������ֵ
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["����ֵ����"].Rows.Add(new string[] { "err", "��ʼ��" });

        //�����Ϸ��Ը�����֤������ʡ��

        //��ʼ�����Ĵ�������ֻ����ʾ������ֱ��������дҵ���߼�������

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@SID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 *  from View_ZZZ_WFSB_list where SID=@SID", "���ݼ�¼", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["���ݼ�¼"].Copy();

            if (redb.Rows.Count < 1)
            {
                dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "ok";//���ok��Ϊ�˱ܿ��Ҳ�����������ʾ
                dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "û���ҵ�ָ������!";
                return dsreturn;
            }
 
            dsreturn.Tables.Add(redb);

            //���ͼƬ���ǿ�ֵ����ͼƬҲŪ����ӽ���
            if (redb.Rows[0]["Sfujian"].ToString() != "")
            {
                //Ttupianpath
                DataTable dttu = new DataTable("ͼƬ��¼");
                dttu.Columns.Add("Ttupianpath");
                string[] arr_tu = redb.Rows[0]["Sfujian"].ToString().Split(',');
                for (int t = 0; t < arr_tu.Length; t++)
                {
                    dttu.Rows.Add(arr_tu[t]);
                }
                dsreturn.Tables.Add(dttu.Copy());

            }

            dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "ok";
            dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "";
        }
        else
        {
            dsreturn.Tables["����ֵ����"].Rows[0]["ִ�н��"] = "err";
            dsreturn.Tables["����ֵ����"].Rows[0]["��ʾ�ı�"] = "������󣬻�ȡʧ�ܣ�" + return_ht["return_errmsg"].ToString();
        }


        return dsreturn;
    }


}
