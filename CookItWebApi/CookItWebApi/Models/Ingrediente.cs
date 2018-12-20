using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookItWebApi.Models
{
    [Table("Ingredientes")]
    public class Ingrediente
    {
        public enum Tipo {
            Aceites = 1,
            Carne = 2,
            Cereales_y_Derivados = 3,
            Cremas_de_untar = 4,
            Especias_y_hierbas = 5,
            Fiambres_y_embutidos = 6,
            Frutas_y_Verduras = 7,
            Frutos_secos_y_semillas = 8,
            Ingredientes_para_hornear = 9,
            Leche_y_productos_lacteos = 10,
            Pastas_y_fideos = 11,
            Pescado_y_mariscos = 12,
            Salsas = 13
        }
        public enum Estacion {
            Verano = 1,
            Otono = 2,
            Invierno = 3,
            Primavera = 4,
            Varios = 5
        }

        //1 = mg, 2 = ml
        public enum TipoMedida {
            ml = 1,
            gr = 2
        }

        [Key]
        public int _Id { set; get; }
        [Required]
        [StringLength(200)]
        public string _Nombre { set; get; }
        [Required]
        [Range(0, Double.PositiveInfinity)]
        public int _Costo { set; get; }

        public TipoMedida _Medida { set; get; }

        [Required]
        [Range(0, int.MaxValue)]
        public int _MedidaPromedio { set; get; }
        [Required]
        [Range(0, int.MaxValue)]
        public int _MedidaPorGramo { set; get; }
        [Required]
        [Range(0, int.MaxValue)]
        public int _CantCaloriasPorMedida { set; get; }
        [Required]
        public bool _AptoCeliacos { set; get; }
        [Required]
        public bool _AptoDiabeticos{ set; get; }
        [Required]
        public bool _AptoVegetarianos { set; get; }
        [Required]
        public bool _AptoVeganos { set; get; }
        [Required]
        public Estacion _Estacion { set; get; }
        [Required]
        public Tipo _Tipo { set; get; }

        //public Ingrediente(int id, string nombre, int costo, int tipoMedida, int medidaProm, int medidaGramo, int caloriasMedida, 
        //    bool aptoCel, bool aptoDia, bool aptoVegetariano, bool aptoVegano, int idTipo, int idEstacion) {
        //    _Id = id;
        //    _Nombre = nombre;
        //    _Costo = costo;
        //    _Medida = (TipoMedida) tipoMedida;
        //    _Estacion = (Estacion) idEstacion;
        //    _Tipo = (Tipo) idTipo;
        //    _MedidaPromedio = medidaProm;
        //    _MedidaPorGramo = medidaGramo;
        //    _CantCaloriasPorMedida = caloriasMedida;
        //    _AptoCeliacos = aptoCel;
        //    _AptoDiabeticos = aptoDia;
        //    _AptoVegetarianos = aptoVegetariano;
        //    _AptoVeganos = _AptoVeganos;
        //}

        //public Ingrediente(string nombre, int costo, int tipoMedida, int medidaProm, int medidaGramo, int caloriasMedida,
        //    bool aptoCel, bool aptoDia, bool aptoVegetariano, bool aptoVegano, int idTipo, int idEstacion){
        //    _Nombre = nombre;
        //    _Costo = costo;
        //    _Medida = (TipoMedida)tipoMedida;
        //    _Estacion = (Estacion)idEstacion;
        //    _Tipo = (Tipo)idTipo;
        //    _MedidaPromedio = medidaProm;
        //    _MedidaPorGramo = medidaGramo;
        //    _CantCaloriasPorMedida = caloriasMedida;
        //    _AptoCeliacos = aptoCel;
        //    _AptoDiabeticos = aptoDia;
        //    _AptoVegetarianos = aptoVegetariano;
        //    _AptoVeganos = _AptoVeganos;
        //}

        //Testeado
        //public bool Insertar()
        //{
        //    SqlConnection cn = ManejadorConexion.CrearConexion();

        //    SqlCommand cmd = new SqlCommand(@"INSERT INTO Ingredientes VALUES (@Nombre, @Costo, @Medida, @MedidaPromedio, @MedidaPorGramo, 
        //                                     @CantCalorias, @AptoCeliacos, @AptoDiabeticos, @AptoVegetarianos, @AptoVeganos, @Tipo, @Estacion); 
        //                                     SELECT SCOPE_IDENTITY()", cn);
        //    cmd.Parameters.Add(new SqlParameter("@Nombre", _Nombre));
        //    cmd.Parameters.Add(new SqlParameter("@Costo", _Costo));
        //    cmd.Parameters.Add(new SqlParameter("@Medida", _Medida.ToString()));
        //    cmd.Parameters.Add(new SqlParameter("@MedidaPromedio", _MedidaPromedio));
        //    cmd.Parameters.Add(new SqlParameter("@MedidaPorGramo", _MedidaPorGramo));
        //    cmd.Parameters.Add(new SqlParameter("@CantCalorias", _CantCaloriasPorMedida));
        //    cmd.Parameters.Add(new SqlParameter("@AptoCeliacos", (bool) _AptoCeliacos));
        //    cmd.Parameters.Add(new SqlParameter("@AptoDiabeticos", (bool)_AptoDiabeticos));
        //    cmd.Parameters.Add(new SqlParameter("@AptoVegetarianos", (bool)_AptoVegetarianos));
        //    cmd.Parameters.Add(new SqlParameter("@AptoVeganos", (bool) _AptoVeganos));
        //    cmd.Parameters.Add(new SqlParameter("@Tipo", (int) _Tipo));
        //    cmd.Parameters.Add(new SqlParameter("@Estacion", (int) _Estacion));
        //    try
        //    {
        //        ManejadorConexion.AbrirConexion(cn);
        //        this._Id = Convert.ToInt32(cmd.ExecuteScalar());
        //        return true;
        //    }
        //    catch (SqlException ex)
        //    {
        //        System.Diagnostics.Debug.Assert(false, ex.Message);
        //        return false;

        //    }
        //    finally
        //    {
        //        ManejadorConexion.CerrarConexion(cn);
        //    }
        //}
        ////Testeado
        //public bool Actualizar()
        //{
        //    SqlConnection cn = ManejadorConexion.CrearConexion();

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = @"UPDATE Ingredientes 
        //                        SET nombre = @Nombre, costo = @Costo, medida = @Medida, medidaPromedio = @MedidaPromedio, 
        //                           medidaPorGramo = @MedidaPorGramo, 
        //                           cantCalorias = @CantCalorias, aptoCeliacos = @AptoCeliacos, aptoDiabeticos = @AptoDiabeticos,
        //                           aptoVegetarianos = @AptoVegetarianos, 
        //                           aptoVeganos = @AptoVeganos, tipo = @Tipo, estacion = @Estacion
        //                        WHERE id = @Id";
        //    cmd.Parameters.Add(new SqlParameter("@Id", _Id));
        //    cmd.Parameters.Add(new SqlParameter("@Nombre", _Nombre));
        //    cmd.Parameters.Add(new SqlParameter("@Costo", _Costo));
        //    cmd.Parameters.Add(new SqlParameter("@Medida", _Medida.ToString()));
        //    cmd.Parameters.Add(new SqlParameter("@MedidaPromedio", _MedidaPromedio));
        //    cmd.Parameters.Add(new SqlParameter("@MedidaPorGramo", _MedidaPorGramo));
        //    cmd.Parameters.Add(new SqlParameter("@CantCalorias", _CantCaloriasPorMedida));
        //    cmd.Parameters.Add(new SqlParameter("@AptoCeliacos", (bool)_AptoCeliacos));
        //    cmd.Parameters.Add(new SqlParameter("@AptoDiabeticos", (bool)_AptoDiabeticos));
        //    cmd.Parameters.Add(new SqlParameter("@AptoVegetarianos", (bool)_AptoVegetarianos));
        //    cmd.Parameters.Add(new SqlParameter("@AptoVeganos", (bool)_AptoVeganos));
        //    cmd.Parameters.Add(new SqlParameter("@Tipo", (int)_Tipo));
        //    cmd.Parameters.Add(new SqlParameter("@Estacion", (int)_Estacion));
        //    cmd.Connection = cn;
        //    try
        //    {
        //        ManejadorConexion.AbrirConexion(cn);
        //        int filas = cmd.ExecuteNonQuery();
        //        return filas == 1;
        //    }
        //    catch (SqlException ex)
        //    {
        //        System.Diagnostics.Debug.Assert(false, ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        ManejadorConexion.CerrarConexion(cn);
        //    }
        //}

        //public bool Borrar()
        //{
        //    return false;
        //}



    }
}
