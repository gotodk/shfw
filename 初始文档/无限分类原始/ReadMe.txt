------------------------------------------------
名称:		叶子无限分类(SQL_Server2K存储过程版)
Name:		YeSort(SQL_Server2K_Procedures)
Release:	0.11.20070331.f
Author:		叶子
Revision:	2007-03-31 14:04:03
Licenses:	GPL(The GNU General Public License)
Descript:	无限分类以及目录树存储过程,unlimited sort & treeview Procedures
WebSite:	http://www.ye.vg

------------------------------------------------
Copy(c):
版权规叶子所有.
这是一款FreeSoftWare(自由软件),您可以在GPL协议许可下修改和派发,商业用途请先行取得作者许可.

------------------------------------------------
Licenses:
本程序遵循GPL(The GNU General Public License)协议.
协议原文地址:http://www.gnu.org/licenses/licenses.html#GPL
协议中文地址:http://www.gnu.org/licenses/licenses.cn.html#GPL


------------------------------------------------
Thanks:
Berk		QQ:606450 E-Mail:berk@263.net 感谢书写asp测试页面以及bug报告.
骑驴觅驴	感谢测试.
星星		感谢部分思路.
裤衩		感谢语句方面的帮助.


------------------------------------------------
Install:
0.默认表名[tbTempSort],如果表名不同,直接批量替换[tbTempSort]即可.
1.执行Install-Tables.sql建表.
2.执行Install-Functions.sql建自定义函数.
3.执行Install-Procedures.sql建存储过程.
4.执行Install-Data.sql添加测试数据.测试数据为mssql企业管理器左侧菜单.
5.各sp调用方法及参数请看存储过程说明.
6.其他附加属性或者字段可以自行添加.
7.如有字段长度变化,对照修改sp里varchar/nvarchar的长度.


------------------------------------------------
Release:
0.11&0.10
1.bug修正.
0.09
1.函数修正替换.
2.存储过程优化.
0.08
1.代码优化,算法更新.
2.分类修正功能,可以修正排序和父级ID.
0.07
1.动态表.
0.06
1.进一步代码规范&优化.
2.加上上下移动级别输出.
3.错误捕捉.
0.05
1.代码规范.
0.04
1.bug修正.
0.03
1.sp初始化以及核心代码.


------------------------------------------------
Return Code:
-111100:未知操作错误
-111101:父级ID不存在
-111102:本级ID不存在
-111103:移动数目错误
-111104:欲转移的ID不存在
-111105:ID为自己的子类
-111106:本级ID已经是父ID的子类了,无需移动
-111107:无分类可供修正

111150:未知操作成功
111151:添加分类成功
111152:修改分类成功
111153:删除分类成功
111154:移动分类排序成功
111155:移动分类目录成功
111156:修正排序成功