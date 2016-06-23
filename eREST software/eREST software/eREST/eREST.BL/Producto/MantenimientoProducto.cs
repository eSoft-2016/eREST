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
            modProducto.PRO_PRECIO = producto.PRO_PRECIO;
            modProducto.PRO_IMAGEN = producto.PRO_IMAGEN;
            modProducto.PRO_FK_RECETA = producto.PRO_FK_RECETA;
            modProducto.PRO_FK_SUBCATEGORIA = producto.PRO_FK_SUBCATEGORIA;
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

        /// <summary>
        /// Listar Categoria    
        /// </summary>
        /// <param name="Listar Categoria"></param>
        public List<string> ListarCategorias()
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<string> categorias = (from c in dc.eREST_CATEGORIAS select c.CAT_NOMBRE).ToList();
            return categorias;
        }
        /// <summary>
        /// Listar SubCategoria    
        /// </summary>
        /// <param name="Lista SubCategoria"></param>
        public List<string> ListarSubCategorias(int idCategoria)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            List<string> subcategorias = (from c in dc.eREST_SUBCATEGORIAS where c.SCA_FK_CATEGORIA == idCategoria select c.SCA_NOMBRE).ToList();
            return subcategorias;
        }

        public int ObtenerIdCategorias(string Categoria)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_CATEGORIAS categorias = (from c in dc.eREST_CATEGORIAS where c.CAT_NOMBRE == Categoria select c).FirstOrDefault();
            return categorias.CAT_PK_CATEGORIA;
        }
        public int ObtenerIdSuCategorias(string SubCategoria)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_SUBCATEGORIAS subcategorias = (from c in dc.eREST_SUBCATEGORIAS where c.SCA_NOMBRE == SubCategoria select c).FirstOrDefault();
            return subcategorias.SCA_PK_SUBCATEGORIA;
        }
        public int AgregarReceta(eREST_RECETAS receta)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            receta.REC_ESTADO = true;
            dc.eREST_RECETAS.InsertOnSubmit(receta);
            dc.SubmitChanges();
            return receta.REC_PK_RECETA;
        }
        public void AgregarDetReceta(eREST_DETRECETAS detreceta)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            dc.eREST_DETRECETAS.InsertOnSubmit(detreceta);
            dc.SubmitChanges();
        }
        public void EditarDetReceta(eREST_DETRECETAS detreceta)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_DETRECETAS modDetalle = dc.eREST_DETRECETAS.FirstOrDefault(c => c.DRE_PK_DETRECETA == detreceta.DRE_PK_DETRECETA);
            modDetalle.DRE_ESTADO = detreceta.DRE_ESTADO;
            modDetalle.DRE_UMEDIDA = detreceta.DRE_UMEDIDA;
            modDetalle.DRE_CANTIDAD = detreceta.DRE_CANTIDAD;
            dc.SubmitChanges();
        }
        public List<eREST_spListarProductosResult> ListarProduto(string pNombre, string pSubCateria)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return dc.eREST_spListarProductos(pNombre, pSubCateria).ToList();
        }
        public void CambiarEstado(int idProduco)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_PRODUCTOS producto = (from c in dc.eREST_PRODUCTOS where c.PRO_PK_PRODUCTO == idProduco select c).FirstOrDefault();
            if (producto.PRO_ESTADO == true) producto.PRO_ESTADO = false;
            else producto.PRO_ESTADO = true;
            dc.SubmitChanges();
        }
        public void CambiarEstadoDetIngrediente(int idDetIngrediente)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_DETRECETAS detReceta = (from c in dc.eREST_DETRECETAS where c.DRE_PK_DETRECETA == idDetIngrediente select c).FirstOrDefault();
            if (detReceta.DRE_ESTADO == true) detReceta.DRE_ESTADO = false;
            else detReceta.DRE_ESTADO = true;
            dc.SubmitChanges();
        }
        public List<eREST_spListarIngredientesResult> ListarIngredientes(int idReceta)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return dc.eREST_spListarIngredientes(idReceta).ToList();
        }
        public bool RevisaIngrediente(int idDetReceta)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_DETRECETAS detReceta = (from c in dc.eREST_DETRECETAS where c.DRE_PK_DETRECETA == idDetReceta select c).FirstOrDefault();
            if (detReceta == null) return false; else return true;
        }
        public List<eREST_DETRECETAS> ListarDetallesPorInsumo(int pkInsumo)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            return (from c in dc.eREST_DETRECETAS where c.DRE_FK_INSUMO == pkInsumo select c).ToList();
        }
        public eREST_PRODUCTOS Consultarproducto(int PK)
        {
            eREST_DiagramaDataContext dc = new eREST_DiagramaDataContext();
            eREST_PRODUCTOS modProducto = dc.eREST_PRODUCTOS.First(c => c.PRO_PK_PRODUCTO == PK);
            return modProducto;
        }
    }
}
