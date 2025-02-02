using System.ComponentModel.DataAnnotations;
using SewPro.Models;

namespace SewPro.Models
{
public class Invoice
{
    public int InvoiceId { get; set; }
    public string InvoiceFooter { get; set; }  // خاصية InvoiceFooter
    // خصائص أخرى...
}}
