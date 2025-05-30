namespace _41_size
{
  using System;
  using System.Collections.Generic;

  public partial class Product
  {
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Product()
    {
      this.OrderProduct = new HashSet<OrderProduct>();
    }

    public string ProductArticleNumber { get; set; }
    public string ProductName { get; set; }
    public string ProductUnit { get; set; }
    public decimal ProductCost { get; set; }
    public Nullable<int> ProductMaxDiscount { get; set; }
    public string ProductManufacturer { get; set; }
    public string ProductImporter { get; set; }
    public string ProductCategory { get; set; }
    public Nullable<byte> ProductDiscountAmount { get; set; }
    public int ProductQuantityInStock { get; set; }
    public string ProductDescription { get; set; }
    public string ProductPhoto { get; set; }

    public string ProductPhoto1
    {
      get
      {
        if (this.ProductPhoto == null)
          return null;
        else
          return "images/" + this.ProductPhoto;
      }
    }

    public string ProductStatus { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<OrderProduct> OrderProduct { get; set; }

    // Добавляем свойство Quantity, которое не связано с базой данных
    public int Quantity { get; set; } = 1; // По умолчанию количество равно 1
  }
}