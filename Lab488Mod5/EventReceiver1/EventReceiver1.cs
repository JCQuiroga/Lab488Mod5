using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace Lab488Mod5.EventReceiver1
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiver1 : SPItemEventReceiver
    {

        private void UpdatePropertyBag(SPWeb web, double cambio)

        {

            string keyName = "TotalFacturas";

            double actual = 0;

            if (web.Properties[keyName] != null)

            {

                actual = double.Parse(web.Properties[keyName]);

            }

            else

            {

                web.Properties.Add(keyName, "");

            }

            actual += cambio;

            web.Properties[keyName] = actual.ToString();

            web.Properties.Update();

        }
        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
           double valor;

            double.TryParse(properties.AfterProperties["Importe"].ToString(), out valor);

            UpdatePropertyBag(properties.Web, valor);
        }

        /// <summary>
        /// An item is being updated.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            double valorPrevio, nuevoValor;

            double.TryParse(properties.ListItem["Importe"].ToString(), out valorPrevio);

            double.TryParse(properties.AfterProperties["Importe"].ToString(), out nuevoValor);

           // double change = valorPrevio - nuevoValor;
            double change = nuevoValor - valorPrevio;

            UpdatePropertyBag(properties.Web, change);
        }

        /// <summary>
        /// An item is being deleted.
        /// </summary>
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            double valor;

            double.TryParse(properties.ListItem["Importe"].ToString(), out valor);

            UpdatePropertyBag(properties.Web, -valor);
        }


    }
}