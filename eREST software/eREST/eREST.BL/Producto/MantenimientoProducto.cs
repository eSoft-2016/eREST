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
        public void AgregarProducto(eREST_PRODUCTO producto)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
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
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
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
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_PRODUCTO modProducto = dc.eREST_PRODUCTOs.First(c => c.PRO_PK_PRODUCTO == producto.PRO_PK_PRODUCTO);
            modProducto.PRO_ESTADO = false;
            dc.SubmitChanges();
        }

        /// <summary>
        /// Listar Categoria    
        /// </summary>
        /// <param name="Listar Categoria"></param>
        public List<string> ListarCategorias()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<string> categorias = (from c in dc.eREST_CATEGORIAs select c.CAT_NOMBRE).ToList();
            return categorias;
        }
        /// <summary>
        /// Listar SubCategoria    
        /// </summary>
        /// <param name="Lista SubCategoria"></param>
        public List<string> ListarSubCategorias()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<string> subcategorias = (from c in dc.eREST_SUBCATEGORIAs select c.SCA_NOMBRE).ToList();
            return subcategorias;
        }
            
    }
}
