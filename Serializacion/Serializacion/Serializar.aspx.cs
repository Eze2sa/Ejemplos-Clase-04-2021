using Serializacion.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace Serializacion
{
    public partial class Serializar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dataArchivo = File.ReadAllText(ConfigurationManager.AppSettings.Get("PathContactos"));

                if (dataArchivo != null)
                {
                    List<Contacto> contactos = JsonConvert.DeserializeObject<List<Contacto>>(dataArchivo);

                    Session["Contactos"] = contactos;
                }
            }
        }

        protected void GuardarSerializando(Object sender, EventArgs e)
        {
            Contacto contacto = new Contacto()
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text
            };
            
            long telefono = -1;
            bool telefonoValido = long.TryParse(txtTelefono.Text, out telefono);

            if(telefono != 0)
            {
                contacto.Telefono = telefono;
            }

            List<Contacto> listaContactos = (List<Contacto>)Session["Contactos"];
            listaContactos.Add(contacto);

            string pathContactos = ConfigurationManager.AppSettings.Get("PathContactos");

            if (File.Exists(pathContactos))
            {
                File.WriteAllText(pathContactos, JsonConvert.SerializeObject(listaContactos));
            }

            Session["Contactos"] = listaContactos;
        }
    }
}