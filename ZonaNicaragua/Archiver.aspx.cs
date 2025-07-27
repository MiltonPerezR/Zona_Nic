using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZonaNicaragua.Models; // O tu namespace real

namespace ZonaNicaragua
{
    public partial class Archiver : System.Web.UI.Page
    {
        public AppDbContext Uow = new AppDbContext();
        

        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in props)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in props)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }

        protected void btnExportarJSON_Click(object sender, EventArgs e)
        {
            var dt = Session["DatosFiltrados"] as DataTable;
            if (dt == null) return;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            Response.Clear();
            Response.ContentType = "application/json";
            Response.AddHeader("content-disposition", "attachment;filename=datos.json");
            Response.Write(json);
            Response.End();
        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            var dt = Session["DatosFiltrados"] as DataTable;
            if (dt == null) return;

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=datos.xls");
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView gv = new GridView { DataSource = dt };
                gv.DataBind();
                gv.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {
            var dt = Session["DatosFiltrados"] as DataTable;
            if (dt == null) return;

            using (MemoryStream ms = new MemoryStream())
            {
                var doc = new Document();
                PdfWriter.GetInstance(doc, ms).CloseStream = false;
                doc.Open();
                var pdfTable = new PdfPTable(dt.Columns.Count);

                foreach (DataColumn col in dt.Columns)
                {
                    pdfTable.AddCell(new Phrase(col.ColumnName));
                }

                foreach (DataRow row in dt.Rows)
                {
                    foreach (var cell in row.ItemArray)
                        pdfTable.AddCell(cell.ToString());
                }

                doc.Add(pdfTable);
                doc.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=datos.pdf");
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }

        protected void btnExportInsert_Click(object sender, EventArgs e)
        {
            export();
        }
        public void export()
        {
            string connStr = WebConfigurationManager.ConnectionStrings["ZonaNic"].ConnectionString;
            StringBuilder sqlScript = new StringBuilder();

            string[] tablas = {
                "M_EPISODIOS", "M_IMAGENH", "M_IMAGENHS", "M_IMAGENV", "M_IMAGENVS",
                "M_PELICULA", "M_SERIE", "M_TIPO_USUARIO", "M_TIPO_VIDEO",
                "M_USUARIO", "Temporadas"
            };

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                foreach (string tabla in tablas)
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM [{tabla}]", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        StringBuilder columnas = new StringBuilder();
                        StringBuilder valores = new StringBuilder();

                        foreach (DataColumn col in dt.Columns)
                        {
                            columnas.Append($"[{col.ColumnName}],");
                            object valor = row[col];

                            if (valor == DBNull.Value)
                                valores.Append("NULL,");
                            else if (col.DataType == typeof(string) || col.DataType == typeof(DateTime))
                                valores.Append($"'{valor.ToString().Replace("'", "''")}',");
                            else
                                valores.Append($"{valor},");
                        }

                        string insert = $"INSERT INTO [{tabla}] ({columnas.ToString().TrimEnd(',')}) VALUES ({valores.ToString().TrimEnd(',')});";
                        sqlScript.AppendLine(insert);
                    }

                    sqlScript.AppendLine(); // Espacio entre tablas
                }

                // Convertimos a bytes para ambos: descarga y adjunto
                byte[] fileBytes = Encoding.UTF8.GetBytes(sqlScript.ToString());


                // También permitir descarga directa
                Response.Clear();
                Response.ContentType = "application/sql";
                Response.AddHeader("Content-Disposition", "attachment; filename=ExportZonaNic.sql");
                Response.BinaryWrite(fileBytes);
                Response.End();
            }
        }

        public void EnviarCorreo(byte[] archivoBytes)
        {
            string remitente = "miltonfocus37@gmail.com";     // Tu correo Gmail
            string contraseña = "hanh kvja tpvd dosj";           // Contraseña de aplicación (NO la normal)
            string destinatario = "mirutondev@gmail.com";     // Destinatario

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(remitente);
            mail.To.Add(destinatario);
            mail.Subject = "Exportación SQL - ZonaNic";
            mail.Body = "Adjunto el archivo de respaldo SQL exportado automáticamente.";

            // Adjuntar archivo desde memoria
            MemoryStream ms = new MemoryStream(archivoBytes);
            Attachment adjunto = new Attachment(ms, "ExportZonaNic.sql", "application/sql");
            mail.Attachments.Add(adjunto);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            //smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(remitente, contraseña);

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al enviar correo: " + ex.Message);
            }
        }

        protected void btnExportInsert_Click1(object sender, EventArgs e)
        {
            string connStr = WebConfigurationManager.ConnectionStrings["ZonaNic"].ConnectionString;
            StringBuilder sqlScript = new StringBuilder();

            string[] tablas = {
                "M_EPISODIOS", "M_IMAGENH", "M_IMAGENHS", "M_IMAGENV", "M_IMAGENVS",
                "M_PELICULA", "M_SERIE", "M_TIPO_USUARIO", "M_TIPO_VIDEO",
                "M_USUARIO", "Temporadas"
            };

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                foreach (string tabla in tablas)
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM [{tabla}]", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        StringBuilder columnas = new StringBuilder();
                        StringBuilder valores = new StringBuilder();

                        foreach (DataColumn col in dt.Columns)
                        {
                            columnas.Append($"[{col.ColumnName}],");
                            object valor = row[col];

                            if (valor == DBNull.Value)
                                valores.Append("NULL,");
                            else if (col.DataType == typeof(string) || col.DataType == typeof(DateTime))
                                valores.Append($"'{valor.ToString().Replace("'", "''")}',");
                            else
                                valores.Append($"{valor},");
                        }

                        string insert = $"INSERT INTO [{tabla}] ({columnas.ToString().TrimEnd(',')}) VALUES ({valores.ToString().TrimEnd(',')});";
                        sqlScript.AppendLine(insert);
                    }

                    sqlScript.AppendLine(); // Espacio entre tablas
                }

                // Convertimos a bytes para ambos: descarga y adjunto
                byte[] fileBytes = Encoding.UTF8.GetBytes(sqlScript.ToString());

                // Enviar al correo como adjunto
                EnviarCorreo(fileBytes);

            }
        }
    }
}
