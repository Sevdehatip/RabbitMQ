using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQProject
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static string connectionString = "amqp://guest:guest@localhost:5672//";

        private IConnection connection;

        private IModel _channel;

        private IModel channel => _channel ?? (_channel = CreateOrGetChannel());

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConnect_Click(object sender, EventArgs e)
        {
            connection = GetConnection();
            Session["conn"] = connection;
        }

        private IConnection GetConnection()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                Uri = new Uri(connectionString, UriKind.RelativeOrAbsolute)
            };
            return factory.CreateConnection();
        }

        protected void btnDeclareExchange_Click(object sender, EventArgs e)
        {
            //öncelikle producerdan bir kanal aracılığıyla exchange gidecek
            if (channel != null)
            {
                channel.ExchangeDeclare(tbExchangeName.Text, ddlExchangeType.SelectedValue, true, false);
            }
        }

        private IModel CreateOrGetChannel()
        {
            connection = (IConnection)Session["conn"];
            if (connection != null)
                return connection.CreateModel();
            else
            {
                lblNotConnect.Visible = true;
                return null;
            }
        }

        protected void btnDeclareQueue_Click(object sender, EventArgs e)
        {
            channel.QueueDeclare(tbqueueName.Text, true, false, false);
        }

        protected void btnBindQueue_Click(object sender, EventArgs e)
        {
            channel.QueueBind(tbqueueName.Text, tbExchangeName.Text, tbRounting.Text);
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            WriteDataToExchange(tbPubExcName.Text, tbPubRoutName.Text, tbmessage.Text);
        }

        private void WriteDataToExchange(string exchangeName, string rootingKey, object data)
        {
            var dataArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));

            channel.BasicPublish(exchangeName, rootingKey, null, dataArr);
        }
    }
}