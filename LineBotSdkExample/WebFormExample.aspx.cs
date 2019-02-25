using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LineBotSdkExample
{
    public partial class WebFormExample : System.Web.UI.Page
    {
        isRock.LineBot.Bot bot = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            //建立bot instance (需要channel access token)
            bot = new isRock.LineBot.Bot(this.txb_Token.Text.Trim());
        }

        protected void Button_SendText_Click(object sender, EventArgs e)
        {
            //傳送純文字訊息
            bot.PushMessage(this.txb_SendTo.Text.Trim(), this.txb_Msg.Text);
        }

        protected void Button_SendSticker_Click(object sender, EventArgs e)
        {
            //傳送貼圖訊息
            bot.PushMessage(this.txb_SendTo.Text.Trim(), int.Parse(this.txb_PackageId.Text), int.Parse(this.txb_StickerId.Text));
        }

        protected void Button_SendPicture_Click(object sender, EventArgs e)
        {
            //傳送圖片訊息
            bot.PushMessage(this.txb_SendTo.Text.Trim(), new Uri(this.txb_PictureURL.Text.Trim()));
        }

        protected void Button_SendButtonTemplate_Click(object sender, EventArgs e)
        {

            //建立actions，作為ButtonTemplate的用戶回覆行為
            var actions = new List<isRock.LineBot.TemplateActionBase>();
            actions.Add(new isRock.LineBot.MessageAction()
            { label = "點選這邊等同用戶直接輸入某訊息", text = "/例如這樣" });
            actions.Add(new isRock.LineBot.UriAction()
            { label = "點這邊開啟網頁", uri = new Uri("http://www.google.com") });
            actions.Add(new isRock.LineBot.PostbackAction()
            { label = "點這邊發生postack", data = "abc=aaa&def=111" });

            //單一Button Template Message
            var ButtonTemplate = new isRock.LineBot.ButtonsTemplate()
            {
                altText = "Line表單)",
                text = txbButtonTemplateText.Text,
                title = txbButtonTemplateTitle.Text,
                //設定圖片
                thumbnailImageUrl = new Uri("https://cdn.clickme.net/gallery/f07b18a48dce9859b3e0bc1584e656bb.jpeg"),
                actions = actions //設定回覆動作
            };

            //發送
            bot.PushMessage(this.txb_SendTo.Text.Trim(), ButtonTemplate);
        }
    }
}