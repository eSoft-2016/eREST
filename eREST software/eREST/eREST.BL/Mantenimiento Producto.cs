using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eREST.BO;
namespace eREST.BL
{
    public class Mantenimiento_Producto
    {
        /// <summary>
        /// Registrar producto
        /// </summary>
        /// <param name="producto"></param>
        public void AgregarProducto(eREST_PRODUCTO producto)
        {
            eREST_DigramaDataContext dc = new eREST_DigramaDataContext();
            producto.PRO_ESTADO = true;
            dc.eREST_PRODUCTOs.InsertOnSubmit(producto);
            dc.SubmitChanges();
        }
        /// <summary>
        /// Registrar producto
        /// </summary>
        /// <param name="producto"></param>
        public void EditarProducto(eREST_PRODUCTO producto)
        {
            eREST_DigramaDataContext dc = new eREST_DigramaDataContext();
            eREST_PRODUCTO modProducto = dc.eREST_PRODUCTOs.First(c => c.PRO_PK_PRODUCTO == producto.PRO_PK_PRODUCTO);
            modProducto.PRO_NOMBRE = producto.PRO_NOMBRE;
            modProducto.PRO_RUTAIMAGEN = producto.PRO_RUTAIMAGEN;
            dc.SubmitChanges();
        }
        /// <summary>
        /// Eliminar producto
        /// </summary>
        /// <param name="producto"></param>
        public void EliminarProducto(eREST_PRODUCTO producto)
        {
            eREST_DigramaDataContext dc = new eREST_DigramaDataContext();
            eREST_PRODUCTO modProducto = dc.eREST_PRODUCTOs.First(c => c.PRO_PK_PRODUCTO == producto.PRO_PK_PRODUCTO);
            modProducto.PRO_ESTADO = false;
            dc.SubmitChanges();
        }
    }
}
}
