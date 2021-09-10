﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReservaCine.Models
{

    public class Cliente : Usuario
    {

        [Required(ErrorMessage = "El campo Fecha de Nacimiento es requerido")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaDeNacimiento { get; set; }


        [Display(Name = "Fecha de Alta")]
        public DateTime FechaDeAlta { get; set; }


        [Display(Name = "Reservas")]
        public List<Reserva> Reservas { get; set; }


   

    }
}