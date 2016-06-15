using FMipcClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class masterpageleftmenu : System.Web.UI.UserControl
{


    #region 用于导航栏控制的委托
    /// <summary>
    /// 设置一个委托，用于控制导航栏内容，获取数据
    /// </summary>
    /// <param name="al_daohang">返回的数据集</param>
    /// <param name="ERRinfo">返回的错误</param>
    public delegate void OnNeedDataHandler(ArrayList al_daohang, string ERRinfo);


    /// <summary>
    /// 设置委托函数的handler key
    /// </summary>
    private static object _on_need_load_data_handler_key = new object();

    /// <summary>
    /// 编写事件,用于对事件处理函数的赋值,其中“OnNeedLoadData "是事件名称，“OnNeedDataHandler“是处理此事件的委托的类型
    /// </summary>
    public event OnNeedDataHandler OnNeedLoadData
    {
        add { Events.AddHandler(_on_need_load_data_handler_key, value); } //为web控件添加事件处理函数
        remove { Events.RemoveHandler(_on_need_load_data_handler_key, value); }//移出此web控件的事件处理函数
    }

    /// <summary>
    /// 用于激活事件的函数，此函数原形需要和该事件对应的委托一致。
    /// </summary>
    /// <param name="al_daohang">返回的数据集</param>
    /// <param name="ERRinfo">返回的错误</param>
    private void RaiseEvent_OnNeedLoadData(ArrayList al_daohang, string ERRinfo)
    {
        OnNeedDataHandler handler = (OnNeedDataHandler)Events[_on_need_load_data_handler_key];//在事件处理列表根据该事件的关键字找出此事件处理函数
        if (handler != null) handler(al_daohang, ERRinfo);//如果有，则执行此事件处理函数
    }

    #endregion


    string menuallhtml = "";
    /// <summary>
    /// 有效追溯名
    /// </summary>
    string s_daohang_name = "";

    /// <summary>
    /// 有效追溯ID
    /// </summary>
    string s_daohang_ID = "";

    /// <summary>
    /// 要过滤掉的无权限最底层菜单id字符串
    /// </summary>
    string del_str = "";

    /// <summary>
    /// 要过滤掉的无权限最底层菜单上级菜单id字符串
    /// </summary>
    string del_str_parents = "";

    /// <summary>
    /// 防止重复匹配
    /// </summary>
    bool zppyc = true;

    protected void Page_Load(object sender, EventArgs e)
    {
         
        /*
         * 数据要求：
         * 每个菜单项必须有个唯一ID，ID中不能含有“[”或“]”或“|”或其他正则表达式特殊字符
         * 尽量不要超过五级，第五级显示上不能再补空位了，会不好看。
         * 每个菜单路径不能互相重复，最好也不要有包含关系。
         */

        //读取后台菜单
        DataTable dtmenu = new DataTable();
        try
        {
            dtmenu.ReadXml(Server.MapPath("/xml/auth_menu_b.xml"));
        }
        catch
        {
            return;
        }

        if (UserSession.是否超管 == "0")
        {
            //读取有效权限枚举
            DataSet dsenum = new DataSet();
            try
            {
                dsenum.ReadXml(Server.MapPath("/xml/auth_enum_number_ANused.xml"));
            }
            catch
            {
                return;
            }

            //获取用户具备权限的枚举(后台菜单权限)
            string in_str = "(";
            Dictionary<string, string> user_have_enum = AuthComm.GetEnumFormUnumber(UserSession.最终权值_后台菜单权限);
            foreach (KeyValuePair<string, string> kv in user_have_enum)
            {
                in_str = in_str + "'" + kv.Value + "',";
            }
            in_str = in_str + "'大补丸')";

            //根据权限枚举，找到要剔除的菜单id(生成字符串del_str) 和 不剔除的菜单 (生成字符串no_del_str)
            DataRow[] dr_need_del = dsenum.Tables["Unumber1"].Select("ANBaseNumber not in " + in_str);
            del_str = "(";
            foreach (DataRow dr in dr_need_del)
            {
                del_str = del_str + dr["ANextendID"].ToString() + ",";
                
            }
            del_str = del_str + "-1)";

            DataRow[] dr_no_del = dtmenu.Select("SortID not in " + del_str + " and m_show='不隐藏'");
            string no_del_str = "(";
            foreach (DataRow dr in dr_no_del)
            {
                DataRow[] dr_tttt = dtmenu.Select("SortParentPath like '%," + dr["SortID"].ToString() + ",%'");
                if (dr_tttt.Length ==0)
                {
                    no_del_str = no_del_str + dr["SortID"].ToString() + ",";
                }
               

            }
            no_del_str = no_del_str + "-1)";

            //有可能要删除的全部上级菜单id集合(tempDic_parentsID_maybe_del)
            DataRow[] dr_may_beneed_del_p = dtmenu.Select("SortID in " + del_str);
            string parentsID_maybe_del = "";
            foreach (DataRow dr in dr_may_beneed_del_p)
            {
                parentsID_maybe_del = parentsID_maybe_del + dr["SortParentPath"].ToString();

            }

            Dictionary<string, string> tempDic_parentsID_maybe_del = new Dictionary<string, string>();
            string[] tt = parentsID_maybe_del.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tt.Length; i++)
            {
                tempDic_parentsID_maybe_del[tt[i]] = "";
            }


            //没有被剔除的菜单所隶属的全部上级菜单id集合，都是绝对不能删除的(tt2)。
            DataRow[] dr_never_del_p = dtmenu.Select("SortID in " + no_del_str);
            string parentsID_never_del = "";
            foreach (DataRow dr in dr_never_del_p)
            {
                parentsID_never_del = parentsID_never_del + dr["SortParentPath"].ToString();
            }
            string[] tt2 = parentsID_never_del.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //在有可能要剔除的菜单中去掉绝对不能被剔除的，就是一定要被剔除的。
            for (int i = 0; i < tt2.Length; i++)
            {
                tempDic_parentsID_maybe_del.Remove(tt2[i]);
            }

            //生成一定要被剔除的select条件字符串(del_str_parents)(此时的tempDic_parentsID_maybe_del已经变成了一定要被剔除的)
            del_str_parents = "(";
            foreach (KeyValuePair<string, string> kv in tempDic_parentsID_maybe_del)
            {
                del_str_parents = del_str_parents + kv.Key + ",";
            }
            del_str_parents = del_str_parents + "-1)";
            


        }
        else
        {
            del_str = "(-1)";
            del_str_parents = "(-1)";
        }



 

        //处理菜单
        menuallhtml = "";
        DataRow[] dr_rootnotes = dtmenu.Select("SortParentID=0 and m_show='不隐藏' and SortID not in " + del_str + " and SortID not in " + del_str_parents, "SortOrder asc");
        LoadDataToTreeView(dtmenu, dr_rootnotes, true, "0", "首页");
        //依据追溯数据替换激活和打开标记
        menuallhtml = Regex.Replace(menuallhtml, @"\[(active)\[("+s_daohang_ID+@")\]\]", "active");
        menuallhtml = Regex.Replace(menuallhtml, @"\[(open)\[(" + s_daohang_ID + @")\]\]", " open");
        menuallhtml = Regex.Replace(menuallhtml, @"\[(active|open)\[(.*?)\]\]", "");
            menuUL.InnerHtml = menuallhtml;

        //改写导航文字
            ArrayList al = new ArrayList();
            al.AddRange(s_daohang_name.Split('|'));
            RaiseEvent_OnNeedLoadData(al, "");


    }



    /// <summary>
    /// 递归遍历节点 加载到树上
    /// </summary>
    /// <param name="dtmenu">所有菜单数据</param>
    /// <param name="DRarr">当前级别菜单列表</param>
    /// <param name="onelevel">是否第一级</param>
    /// <param name="allparentsID">每层父级追溯ID</param>
    /// <param name="allparentsName">每层父级追溯名</param>
    private void LoadDataToTreeView(DataTable dtmenu, DataRow[] DRarr, bool onelevel, string allparentsID, string allparentsName)
    {

      
        int allzi = DRarr.Length;
        if (!onelevel)
        {
            menuallhtml = menuallhtml + "<ul class='submenu'>";
        }
        //循环本级菜单
        for (int t = 0; t < allzi; t++)
        {
            string next_allparentsID = allparentsID;
            string next_allparentsName = allparentsName;
            DataRow drRoot_nowzi = DRarr[t];
            //检查菜单显示，隐藏禁止查看的菜单。

            //下属菜单预提取
            DataRow[] dr_nextnotes = dtmenu.Select("SortParentID=" + DRarr[t]["SortID"].ToString() + " and m_show='不隐藏' and SortID not in " + del_str + " and SortID not in " + del_str_parents, "SortOrder asc");
            int dr_nextnotesCount = dr_nextnotes.Count();

            //当前菜单是否使用激活和打开样式
            string css_active = "[active[" + DRarr[t]["SortID"].ToString() + "]]"; //初始化标记
            string css_open = "[open[" + DRarr[t]["SortID"].ToString() + "]]"; //初始化标记

            next_allparentsID = next_allparentsID + "|" + DRarr[t]["SortID"].ToString(); //累加
            next_allparentsName = next_allparentsName + "|" + DRarr[t]["SortName"].ToString(); //累加
            if (dr_nextnotesCount < 1)
            {
                css_open = "";

                //是否存在匹配的高亮跟踪地址
                bool H_M = false;
                string[] formenu = DRarr[t]["m_url_formenu_g"].ToString().Split('★');

           
                for (int m = 0; m < formenu.Length; m++)
                {
                    string Pattern = formenu[m].Trim();
                    Regex r = new Regex(Pattern,RegexOptions.IgnoreCase);
                    string source = Request.Url.ToString();
                    Match PPP = r.Match(source);

                    if (formenu[m].Trim() != "" && PPP.Success)
                    {
                        H_M = true;
                    }
                }

                ////尝试匹配来源地址,只匹配一次
                if (DRarr[t]["m_url"].ToString().Trim() != "" && Request.Url.ToString().IndexOf(DRarr[t]["m_url"].ToString()) < 0 && !H_M)
                {
                    if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().Trim() != "" && Request.UrlReferrer.ToString().IndexOf(DRarr[t]["m_url"].ToString()) >= 0 && zppyc)
                    {
                        H_M = true;
                    }
                }

                if (DRarr[t]["m_url"].ToString().Trim() != "" && (Request.Url.ToString().IndexOf(DRarr[t]["m_url"].ToString()) >= 0 || H_M))
                {
                    //发现这个项目没有子菜单并且链接跟浏览器地址相符，记录下这个有效的每层父级追溯
                    s_daohang_ID = next_allparentsID;
                    s_daohang_name = next_allparentsName;
                    zppyc = false;
                }
          
            }
        
            

            //是否有下级菜单的样式处理
            string css_havesub_arrow = "";
            string css_havesub_toggle = "";
            if (dr_nextnotesCount > 0)
            {
                css_havesub_arrow = "<b class='arrow fa fa-angle-down'></b>";
                css_havesub_toggle = "class='dropdown-toggle'";
            }

            //只让第一级菜单的图标有效
            string css_tubiao = "";
            if (!onelevel)
            {
                css_tubiao = "fa-caret-right";
            }
            else
            {
                css_tubiao = DRarr[t]["m_ico"].ToString();
            }

        
            //判定当前是否最后一层并且是否应该激活

            //开始拼接html代码
            menuallhtml = menuallhtml + "<li class='" + css_active + css_open + "'>";
            menuallhtml = menuallhtml + "<a href='" + DRarr[t]["m_url"].ToString() + "' " + css_havesub_toggle + ">";
            menuallhtml = menuallhtml + "<i class='menu-icon fa " + css_tubiao + "'></i>";
            menuallhtml = menuallhtml + "<span class='menu-text'> " + DRarr[t]["SortName"].ToString() + " </span>";
            menuallhtml = menuallhtml + css_havesub_arrow;
            menuallhtml = menuallhtml + "</a>";
            menuallhtml = menuallhtml + "<b class='arrow'></b>";

            //递归
            if (dr_nextnotesCount > 0)
            {

                LoadDataToTreeView(dtmenu, dr_nextnotes, false, next_allparentsID, next_allparentsName);
            }
            //补上结尾html代码
            menuallhtml = menuallhtml + "</li>";


        }
        if (!onelevel)
        {
            //补上结尾html代码
            menuallhtml = menuallhtml + "</ul>";
        }

    }
 


}