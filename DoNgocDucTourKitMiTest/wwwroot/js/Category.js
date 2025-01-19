var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $.ajax({
        type: 'GET',
        url: '/Category/GetAll',
        mimeType: 'json',
        success: function (data) {
            $.each(data, function (i, data) {
                console.log(data);
                var body = "<tr>";
                body += "<td>" + data.id + "</td>";
                body += "<td>" + data.name + "</td>";
                body += "<td>" + data.numberProducts + "</td>";
                body += "<td>" + new Date(data.createdDate).toLocaleDateString() + "</td>";
                body += "<td>" + `<a href="/Category/AddUpdate?id=${data.id}" class="btn btn-primary mx-2">  <i class="fa-solid fa-pen-to-square"></i> Edit</a>
                     <a onclick="return confirm('Do you really want to delete this? ')" href="/Category/Delete?id=${data.id}" class="btn btn-danger mx-2"> <i class="fa-solid fa-trash"></i> Delete</a>`   + "</td>";
                body += "</tr>";
                $("#tableCategory tbody").append(body);
            });
            /*DataTables instantiation.*/
            $("#tableCategory").DataTable();
        },
        error: function () {
            alert('Fail!');
        }
    });


}
