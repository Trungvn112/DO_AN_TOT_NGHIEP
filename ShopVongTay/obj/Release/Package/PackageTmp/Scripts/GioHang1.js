$(document).ready(function () {
    DatHang = function (MaSP) {
        var SL = $('#txtSL').val();
        if (isNaN(SL) == true) {
            SL = 1;
        }
        var check = $('#username').html();
        if (check == "") {
            alert("Bạn cần đăng nhập để thực hiện chức năng này!");
            location.href = "/Home/DangNhap";
        }
        else {
            $.ajax({
                url: "/GioHang/ThemGiohang",
                data: { iMaSP: MaSP, SL: SL },
                type: "POST",
                success: function (result) {
                    var TongSL = 0;
                    var TongTien = 0;

                    $.each(result, function (i, item) {
                        TongSL += item.SoLuongMua;
                        TongTien += item.TongTien
                    });
                    //$('#cart > a  > span').html(TongSL + " Sản Phẩm - " + TongTien + "VND")
                    $('.cart-icon > span').html(TongSL)
                    $('.cart-price > span').html(TongTien + ".VNĐ")
                    alert('Đặt hàng thành công!')


                }
            });
            return false;
        }
     
    };

    Popup = (function (MaSP) {

        $.ajax({
            url: "/Home/Popup",
            data: { iMaSP: MaSP },
            type: "GET",
            dataType: "json",
            contentType: "Student",
            success: function (result) {
                var item = "";
                $('.modal-body').empty();

                var hinh = result.hinhminhhoa;
                var R =
                    "<div class='left'>"
                    + "<img src = '/img/sanpham/" + hinh + "'/>"
                    + "<div class='right'>"
                    + "<p id='pname'>" + result.name + "</p>"
                    + "<p><strong>Đơn giá:</strong> " + result.price + ".VNĐ </p>"
                    + "<p><strong>Đơn vị tính:</strong> " + result.donvitinh + "</p>"
                    + "<p id='popupmota'><strong>Số lượng:</strong> " + result.soluong + "</p>"
                    + "<p><strong>Trạng thái:</strong> " + result.trangthai + "</p>"
                    + "<button id='popupbutton' onclick='DatHang(" + result.masp + ")'>Thêm vào giỏ hàng</button> Hoặc <a href='/ViewSP/ChiTietSanPham?MaSP=" + result.masp + "'>Xem chi tiết</a>"
                    + "</div>"


                $('.modal-body').append(R);
                modelz.style.display = "block";

            }

        });
        return false;
    });

    CapNhat = (function (MaSP) {
        SL = $("#" + MaSP).val();
        if (isNaN(SL) == true) {
            SL = 1;
        }
        var TongTien1 = 0;
        var TongSL1 = 0
        $.ajax({
            url: "/GioHang/ThemGiohang",
            data: { iMaSP: MaSP, SL: SL },
            type: "POST",
            success: function (result) {
                var item = "";
                $('.cart-table tbody').empty();
                $.each(result, function (i, item) {
                    var R = "<tr>"
                        //+ "<td><input disabled  type='text' value=" + item.MaSP + " id='MaSach' name='MaSach'></td>"
                        //  <td class="cart-pic first-row"><img src="~/img/sanpham/@item.HinhAnh" alt=""></td>//
                        + "<td ><img src='/img/sanpham/" + item.HinhAnh + "' /></td>"
                        + "<td>" + item.TenSP + "</td>"
                        + "<td>" + item.DonGia + "</td>"
                        + "<td><input type='text' id='" + item.MaSP + "' class='txtSLGH' value=" + item.SoLuongMua + " /><a class='fa fa-upload btn-Update' onclick='CapNhat(" + item.MaSP + ")' aria-hidden='true'></a><a  class='fa fa-trash-o btn-Delete' aria-hidden='true' onclick=' XoaSP(" + item.MaSP + ")'></a></td>"
                        
                        + "<td>" + item.TongTien + "</td>"
                        + "</tr>"
                    
                    TongTien1 += item.TongTien;
                    TongSL1 += item.SoLuongMua;
                    alert("Thành Công ");
                    $('.cart-table tbody').append(R);
                });
                $('.TongTien > h2 > span').html(TongTien1)
                //$('#cart > a  > span').html(TongSL1 + " Sản Phẩm - " + TongTien1 + "VND")
                $('.cart-icon > span').html(TongSL1)
                $('.cart-price > span').html(TongTien1 + ".VNĐ")
                //$('.rightz > span').html("<p style='color:red; display:inline-block;padding:0;margin:0;'>" + TongSL1 + "</p> Sản phẩm - <p style='color:red;display:inline-block;padding:0;margin:0;'>" + TongTien1 + "</p> VNĐ")
                $('#tongtien').html(TongTien1 + ".VNĐ")
            },
        });
        return false;
    });
    XoaSP = (function (MaSP) {
        $.ajax({
            url: "/GioHang/XoaSP",
            data: { iMaSP: MaSP },
            type: "POST",
            success: function (result) {
                var item = "";
                var TongTien = 0;
                var TongSL2 = 0;
                $('.cart-table tbody').empty();
                $.each(result, function (i, item) {
                    var R = "<tr>"
                        + "<td><input disabled  type='text' value=" + item.MaSP + " id='MaSach' name='MaSach'></td>"
                        + "<td><a href='/ViewSach/ChiTietSach?MaSach=@item.MaSP'><img src='/Images/" + item.HinhAnh+ "' /></a></td>"
                        + "<td>" + item.TenSP + "</td>"
                        + "<td><input type='text' id='" + item.MaSP + "' class='txtSLGH' value=" + item.SoLuongMua + " /><a class='fa fa-upload btn-Update' onclick='CapNhat(" + item.MaSP + ")' aria-hidden='true'></a><a  class='fa fa-trash-o btn-Delete' aria-hidden='true' onclick=' XoaSP(" + item.MaSP + ")'></a></td>"
                        + "<td>" + item.DonGia + "</td>"
                        + "<td>" + item.TongTien + "</td>"
                        + "</tr>"
                    TongTien += item.TongTien
                    TongSL2 += item.SoLuongMua
                    $('.cart-table tbody').append(R);

                });
                $('.TongTien > h2 > span').html(TongTien)
                $('.rightz > span').html("<p style='color:red; display:inline-block;padding:0;margin:0;'>" + TongSL2 + "</p> Sản phẩm - <p style='color:red;display:inline-block;padding:0;margin:0;'>" + TongTien + "</p> VNĐ")
                $('#tongtien').html(TongTien)
            },
        });
        return false;
    });
    tai = function () {
        alert("Load lại")
    };
});
