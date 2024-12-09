using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Transfer
{
    public int TransferId { get; set; }

    public string Reference { get; set; }

    public int TransferFrom { get; set; }

    public int TransferTo { get; set; }

    public string TransferStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public Warehouse? FromWarehouse { get; set; }

    [JsonIgnore]
    public Warehouse? ToWarehouse { get; set; }
}
