$('#btntimkiem').on('click', function () {
    var giatrinhapvao = $('#searchcontent').val();
    if ($('#searchcontent').val() == " ") {
        alert("nhapp");
    }
    else {
       
       
        window.location.href = "/Home/TimKiem?name=" + giatrinhapvao;
    }
    
});
