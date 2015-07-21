import-module mdbc

Connect-Mdbc . ProductDataProvider products

[xml]$books = gc "C:\git\active-commerce-training\src\ActiveCommerce.Training.ProductImport\App_Data\books.xml"

foreach($book in $books.catalog.book)
{
    @{
        id = $book.id
        author = $book.author
        title = $book.title
        genre = $book.genre
        price = $book.price
        publishDate = $book.publish_date
        description = $book.description
        stock = $book.stock
        sku = $book.sku
        weight = $book.weight
    } | Add-MdbcData
}

$data = Get-MdbcData
$data