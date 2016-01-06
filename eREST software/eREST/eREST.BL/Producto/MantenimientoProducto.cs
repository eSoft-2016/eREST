using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eREST.BO;
using System.Data.Linq;

namespace eREST.BL.Producto
{
    public class MantenimientoProducto
    {
        /// <summary>
        /// Registrar producto
        /// </summary>
        /// <param name="producto"></param>
        public void AgregarProducto(eREST_PRODUCTOS producto)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            producto.PRO_ESTADO = true;
            dc.eREST_PRODUCTOS.InsertOnSubmit(producto);
            dc.SubmitChanges();
        }
        /// <summary>
        /// Registrar producto
        /// </summary>
        /// <param name="producto"></param>
        public void EditarProducto(eREST_PRODUCTOS producto)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_PRODUCTOS modProducto = dc.eREST_PRODUCTOS.First(c => c.PRO_PK_PRODUCTO == producto.PRO_PK_PRODUCTO);
            modProducto.PRO_NOMBRE = producto.PRO_NOMBRE;
            modProducto.PRO_RUTAIMAGEN = producto.PRO_RUTAIMAGEN;
            dc.SubmitChanges();
        }
        /// <summary>
        /// Eliminar producto
        /// </summary>
        /// <param name="producto"></param>
        public void EliminarProducto(eREST_PRODUCTOS producto)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_PRODUCTOS modProducto = dc.eREST_PRODUCTOS.First(c => c.PRO_PK_PRODUCTO == producto.PRO_PK_PRODUCTO);
            modProducto.PRO_ESTADO = false;
            dc.SubmitChanges();
        }
    }
}
