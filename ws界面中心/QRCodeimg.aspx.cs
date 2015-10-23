using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing.Imaging;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using System.IO;
using System.Text;

public partial class QRCodeimg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["rqstr"] == null)
        {
            return;
        }
        QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

        qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;

        qrCodeEncoder.QRCodeScale = 4;

        qrCodeEncoder.QRCodeVersion = 8;

        qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

        //String data = "Hello 二维码！";
        String data = Request["rqstr"].ToString();
        //Response.Write(data);

        System.Drawing.Bitmap image = qrCodeEncoder.Encode(data);

        System.IO.MemoryStream MStream = new System.IO.MemoryStream();

        image.Save(MStream, System.Drawing.Imaging.ImageFormat.Png);

        Response.ClearContent();

        Response.ContentType = "image/Png";

        Response.BinaryWrite(MStream.ToArray());
    }
}