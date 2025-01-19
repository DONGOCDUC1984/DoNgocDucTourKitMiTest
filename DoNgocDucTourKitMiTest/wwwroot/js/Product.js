var dataTable;
$(document).ready(function () {
    loadDataTable();
});

$("#CategoryId").change(function () {
    //loadDataTable();
});
function loadDataTable() {
    $.ajax({
        type: 'GET',
        url: '/Home/GetAll',
        mimeType: 'json',
        success: function (data) {
            console.log("Selected CategoryId: " + $('#CategoryId').val()); 
            console.log("products: " + JSON.stringify(data) );
            //if (parseInt($('#CategoryId').val()) != null) {
            //    data = data.filter((x) => x.categoryId === parseInt($('#CategoryId').val()));
            //}
            
            /*console.log("Filtered products: "+data);*/
            $.each(data, function (i, data) {
                var body = "<tr>";
                body += "<td>" + data.id + "</td>";
                body += "<td>" + data.name + "</td>";
                body += "<td>" + data.price + "</td>";
                body += "<td>" + data.category.name + "</td>";
                body += "<td>" + new Date(data.createdDate).toLocaleDateString() + "</td>";
                body += "<td>" + `<a href="/Home/AddUpdate?id=${data.id}" class="btn btn-primary mx-2">  <i class="fa-solid fa-pen-to-square"></i> Edit</a>
                     <a onclick="return confirm('Do you really want to delete this? ')" href="/Home/Delete?id=${data.id}" class="btn btn-danger mx-2"> <i class="fa-solid fa-trash"></i> Delete</a>` + "</td>";
                body += "</tr>";
                $("#tableProduct tbody").append(body);
            });
            /*DataTables instantiation.*/
            $("#tableProduct").DataTable();
        },
        error: function () {
            alert('Fail!');
        }
    });


}
