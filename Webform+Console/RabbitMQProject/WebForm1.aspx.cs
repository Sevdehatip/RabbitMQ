using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RabbitMQ.Client;

namespace RabbitMQProject
{
    public partial class WebForm1 : System.Web.UI.Page
    {
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
                Uri = new Uri("amqp://guest:guest@localhost:5672//", UriKind.RelativeOrAbsolute)
            };
            return factory.CreateConnection();
        }

        protected void btnDeclareExchange_Click(object sender, EventArgs e)
        {
            //öncelikle producerdan bir kanal aracılığıyla exchange gidecek
            if (channel != null)
            {
                channel.ExchangeDeclare(tbExchangeName.Text, ddlExchangeType.SelectedValue);
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
    }
}