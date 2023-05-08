$(document).on("click", ".modal-btn", function (e) {
    e.preventDefault();

    let url = $(this).attr("href");

    fetch(url)
        .then(response => {

            console.log(response)
            if (response.ok) {
                return response.text()
            }
            else {
                alert("Bilinmedik bir xeta bas verdi!")
            }
        })
        .then(data => {
            //$("#quickModal .product-title").text(data.book.name)
            $("#quickModal .modal-dialog").html(data)
            $("#quickModal").modal('show');
        })
    //get date
    //data set to modal
    //modal show
})