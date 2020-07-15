Thongke = (function () {
    var year = document.getElementById("year");
    var month = document.getElementById("month");
    var yearitem = year.options[year.selectedIndex].text;
    var monthitem = month.options[month.selectedIndex].text;
    window.location.href = "/Admin/ThongKe/ThongKe?imonth=" + monthitem + "&iyear=" + yearitem
    
});

HoaDon = (function () {
    var year = document.getElementById("nam");
    var month = document.getElementById("thang");
    var day = document.getElementById("ngay");
    var yearitem = year.options[year.selectedIndex].text;
    var monthitem = month.options[month.selectedIndex].text;
    var dayitem = day.options[day.selectedIndex].text;
    if (dayitem == "Chọn Ngày") {
        window.location.href = "/Admin/ThongKe/HoaDonThang?year=" + yearitem + "&month=" + monthitem
    }
    else if (monthitem == "Chọn Tháng" || dayitem == "Chọn Ngày") {
        window.location.href = "/Admin/ThongKe/HoaDonNam?year=" + yearitem

    }
    else {
        window.location.href = "/Admin/ThongKe/HoaDonNgay?day=" + dayitem + "&year=" + yearitem + "&month=" + monthitem

    }
});