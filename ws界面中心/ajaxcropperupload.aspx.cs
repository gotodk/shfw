using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
///用于解析json字符串
/// </summary>
public class cropperForm_avatar_data
{
    public float x { get; set; }
    public float y { get; set; }
    public float height { get; set; }
    public float width { get; set; }
    public float rotate { get; set; }
}
public partial class ajaxcropperupload : System.Web.UI.Page
{
    /// <summary>
    /// 生成json
    /// </summary>
    /// <param name="state"></param>
    /// <param name="message"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    protected string getjson(string state, string message, string result)
    {
        return "{\"state\":\"" + state + "\",\"message\":\"" + message + "\",\"result\":\"" + result + "\"}";
    }
    
    /// <summary>
    /// 以逆时针为方向对图像进行旋转
    /// </summary>
    /// <param name="b">位图流</param>
    /// <param name="angle">旋转角度[0,360](前台给的)</param>
    /// <returns></returns>
    protected Bitmap Rotate(Bitmap b, int angle)
    {
        angle = angle % 360;            //弧度转换
        double radian = angle * Math.PI / 180.0;
        double cos = Math.Cos(radian);
        double sin = Math.Sin(radian);
        //原图的宽和高
        int w = b.Width;
        int h = b.Height;
        int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
        int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
        //目标位图
        Bitmap dsImage = new Bitmap(W, H);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //计算偏移量
        Point Offset = new Point((W - w) / 2, (H - h) / 2);
        //构造图像显示区域：让图像的中心与窗口的中心点一致
        Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
        Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
        g.TranslateTransform(center.X, center.Y);
        g.RotateTransform(360 - angle);
        //恢复图像在水平和垂直方向的平移
        g.TranslateTransform(-center.X, -center.Y);
        g.DrawImage(b, rect);
        //重至绘图的所有变换
        g.ResetTransform();
        g.Save();
        g.Dispose();
        b.Dispose();
        //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        return dsImage;
    }


    /// <summary>
    /// 按比例缩放图片
    /// </summary>
    /// <param name="b">原图片</param>
    /// <param name="destHeight">新高度</param>
    /// <param name="destWidth">新宽度</param>
    /// <returns></returns>
    protected Bitmap GetThumbnail(Bitmap b, int destHeight, int destWidth)
    {

        System.Drawing.Image imgSource = b;

        System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;

        int sW = 0, sH = 0;

        // 按比例缩放           

        int sWidth = imgSource.Width;

        int sHeight = imgSource.Height;

        if (sHeight > destHeight || sWidth > destWidth)
        {

            if ((sWidth * destHeight) > (sHeight * destWidth))
            {

                sW = destWidth;

                sH = (destWidth * sHeight) / sWidth;

            }

            else
            {

                sH = destHeight;

                sW = (sWidth * destHeight) / sHeight;

            }

        }

        else
        {

            sW = sWidth;

            sH = sHeight;

        }

        Bitmap outBmp = new Bitmap(destWidth, destHeight);

        Graphics g = Graphics.FromImage(outBmp);

        g.Clear(Color.Transparent);

        // 设置画布的描绘质量         

        g.CompositingQuality = CompositingQuality.Default;

        g.SmoothingMode = SmoothingMode.Default;

        g.InterpolationMode = InterpolationMode.Default;

        g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);

        g.Dispose();

        // 以下代码为保存图片时，设置压缩质量     

        EncoderParameters encoderParams = new EncoderParameters();

        long[] quality = new long[1];

        quality[0] = 70;

        EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

        encoderParams.Param[0] = encoderParam;

        imgSource.Dispose();

        return outBmp;

    }

    protected void Page_Load(object sender, EventArgs e)
    {

   
        //{"state":"500","message":"失败","result":""}
        //{"state":"200","message":"成功","result":"生成的服务器路径"}

        int maxpx = 700;
 
        try
        {
            //登录状态判定
            if (UserSession.唯一键 == "")
            {
                Response.Write(getjson("500", "失败：尚未登录，未能识别当前用户身份", ""));
                return;
            }

            cropperForm_avatar_data Cavatar_data = null;
            if (Request["avatar_data"] != null)
            {
                string avatar_data = System.Web.HttpUtility.UrlDecode(Request["avatar_data"].ToString().Trim());
                JavaScriptSerializer js = new JavaScriptSerializer();
                Cavatar_data = js.Deserialize<cropperForm_avatar_data>(avatar_data);
            }

            //DataTable d = RequestForUI.Get_parameter_forUI(Request);

            string hhbiaozhi = Request["hhbiaozhi"].ToString();


            if ((hhbiaozhi == "upandre" || hhbiaozhi == "onlyre") && Cavatar_data == null)
            {
                Response.Write(getjson("500", "失败：没有收到修剪参数", ""));
                return;
            }

            if ((hhbiaozhi == "upandre" || hhbiaozhi == "onlyupload") && Request.Files.Count < 1)
            {
                Response.Write(getjson("500", "失败：没有文件待上传",""));
                return;
            }

            if (hhbiaozhi == "upandre")
            {
                HttpPostedFile file = Request.Files[0];

                int fileSizeInBytes = file.ContentLength;
                //判定文件大小是否符合要求(10M,实际限制在前台控制就行了)
                if (fileSizeInBytes > 10485760)
                {
                    Response.Write(getjson("500", "失败：文件过大，上限10M", ""));
                    return;
                }


                string fileName = file.FileName;
                string fileExtension = Path.GetExtension(fileName);
                if (fileExtension == null || fileExtension == String.Empty)
                {
                    Response.Write(getjson("500", "失败：不允许的文件类型", ""));
                    return;
                }
                fileExtension = fileExtension.ToLower();
                string leixing = "[.jpg][.bmp][.jpeg][.gif][.png]";
                if (leixing.IndexOf("[" + fileExtension + "]") < 0)
                {
                    Response.Write(getjson("500", "失败：不允许的文件类型", ""));
                    return;
                }

                string monthStr = DateTime.Now.Month.ToString();
                if (int.Parse(monthStr) < 10)
                { monthStr = "0" + monthStr; }


                string DayStr = DateTime.Now.Day.ToString();
                if (int.Parse(DayStr) < 10)
                {
                    DayStr = "0" + DayStr;
                }



                string saveDBpath = "/uploadfiles/" + DateTime.Now.Year.ToString() + "/" + monthStr + "/" + DayStr + "/";
                string saveDBname = CombGuid.GetNewCombGuid("img") + fileExtension;
                string UploadURL = Server.MapPath(saveDBpath);//上传的目录



                if (!Directory.Exists(UploadURL))//判断文件夹是否已经存在
                {
                    Directory.CreateDirectory(UploadURL);//创建文件夹
                }

                string savedFileName = UploadURL + saveDBname;
                file.SaveAs(savedFileName + ".temp");

                //根据传入的参数，对图片进行二次处理
                Bitmap bitmap = new Bitmap(savedFileName + ".temp");

                int x = Convert.ToInt32(Math.Round(Cavatar_data.x, 0));
                int y = Convert.ToInt32(Math.Round(Cavatar_data.y, 0));
                int width = Convert.ToInt32(Math.Round(Cavatar_data.width, 0));
                int height = Convert.ToInt32(Math.Round(Cavatar_data.height, 0));
                int rotate = Convert.ToInt32(Math.Round(Cavatar_data.rotate, 0));
                bitmap = Rotate(bitmap, -rotate);


                Graphics g_o = Graphics.FromImage(bitmap);
                Bitmap bt = new Bitmap(width, height, g_o);

                Graphics grahics = Graphics.FromImage(bt);
                Rectangle srcRect = new Rectangle(x, y, width, height);
                GraphicsUnit units = GraphicsUnit.Pixel;
                grahics.DrawImage(bitmap, 0, 0, srcRect, units);

                string re_width = width.ToString();
                string re_height = height.ToString();
                if (width > maxpx)
                {
                    bt = GetThumbnail(bt, (maxpx * height) / width, maxpx);
                    re_width = maxpx.ToString();
                    re_height = ((maxpx * height) / width).ToString("#.##");
                }




                bt.Save(savedFileName);
                bitmap.Dispose();
                grahics.Dispose();
                g_o.Dispose();
                bt.Dispose();
                File.Delete(savedFileName + ".temp");


                Response.Write(getjson("200", re_width.ToString() + "," + re_height.ToString(), saveDBpath + saveDBname));
                return;
            }
            if (hhbiaozhi == "onlyupload")
            {
                HttpPostedFile file = Request.Files[0];

                int fileSizeInBytes = file.ContentLength;
                //判定文件大小是否符合要求(10M,实际限制在前台控制就行了)
                if (fileSizeInBytes > 10485760)
                {
                    Response.Write(getjson("500", "失败：文件过大，上限10M", ""));
                    return;
                }


                string fileName = file.FileName;
                string fileExtension = Path.GetExtension(fileName);
                if (fileExtension == null || fileExtension == String.Empty)
                {
                    Response.Write(getjson("500", "失败：不允许的文件类型", ""));
                    return;
                }
                fileExtension = fileExtension.ToLower();
                string leixing = "[.jpg][.bmp][.jpeg][.gif][.png]";
                if (leixing.IndexOf("[" + fileExtension + "]") < 0)
                {
                    Response.Write(getjson("500", "失败：不允许的文件类型", ""));
                    return;
                }

                string monthStr = DateTime.Now.Month.ToString();
                if (int.Parse(monthStr) < 10)
                { monthStr = "0" + monthStr; }


                string DayStr = DateTime.Now.Day.ToString();
                if (int.Parse(DayStr) < 10)
                {
                    DayStr = "0" + DayStr;
                }



                string saveDBpath = "/uploadfiles/" + DateTime.Now.Year.ToString() + "/" + monthStr + "/" + DayStr + "/";
                string saveDBname = "tempadel_"+CombGuid.GetNewCombGuid("img") + fileExtension;
                string UploadURL = Server.MapPath(saveDBpath);//上传的目录



                if (!Directory.Exists(UploadURL))//判断文件夹是否已经存在
                {
                    Directory.CreateDirectory(UploadURL);//创建文件夹
                }

                string savedFileName = UploadURL + saveDBname;
                file.SaveAs(savedFileName);

                

                Response.Write(getjson("200", "", saveDBpath + saveDBname));
                return;
            }
            if (hhbiaozhi == "onlyre")
            {
                 
                string monthStr = DateTime.Now.Month.ToString();
                if (int.Parse(monthStr) < 10)
                { monthStr = "0" + monthStr; }


                string DayStr = DateTime.Now.Day.ToString();
                if (int.Parse(DayStr) < 10)
                {
                    DayStr = "0" + DayStr;
                }


                string reurl = Request["reurl"].ToString();
                string old_allpath = Server.MapPath(reurl);
                string saveDBpath = Path.GetDirectoryName(old_allpath) + "\\";
                string saveDBname = Path.GetFileName(old_allpath).Replace("tempadel_", "");
                string new_rul = reurl.Replace("tempadel_", "");
           

  

                //根据传入的参数，对图片进行二次处理
                Bitmap bitmap = new Bitmap(old_allpath);

                int x = Convert.ToInt32(Math.Round(Cavatar_data.x, 0));
                int y = Convert.ToInt32(Math.Round(Cavatar_data.y, 0));
                int width = Convert.ToInt32(Math.Round(Cavatar_data.width, 0));
                int height = Convert.ToInt32(Math.Round(Cavatar_data.height, 0));
                int rotate = Convert.ToInt32(Math.Round(Cavatar_data.rotate, 0));
                bitmap = Rotate(bitmap, -rotate);


                Graphics g_o = Graphics.FromImage(bitmap);
                Bitmap bt = new Bitmap(width, height, g_o);

                Graphics grahics = Graphics.FromImage(bt);
                Rectangle srcRect = new Rectangle(x, y, width, height);
                GraphicsUnit units = GraphicsUnit.Pixel;
                grahics.DrawImage(bitmap, 0, 0, srcRect, units);

                string re_width = width.ToString();
                string re_height = height.ToString();
                if (width > maxpx)
                {
                    bt = GetThumbnail(bt, (maxpx * height) / width, maxpx);
                    re_width = maxpx.ToString();
                    re_height = ((maxpx * height) / width).ToString("#.##");
                }




                bt.Save(saveDBpath + saveDBname);
                bitmap.Dispose();
                grahics.Dispose();
                g_o.Dispose();
                bt.Dispose();
                try
                {
                    File.Delete(old_allpath);
                }
                catch { }



                Response.Write(getjson("200", re_width.ToString() + "," + re_height.ToString(), new_rul));
                return;
            }
        }
        catch (Exception ex)
        {
            Response.Write(getjson("500", "失败：服务器错误", ""));
            return;
        }

       
         


    }
}