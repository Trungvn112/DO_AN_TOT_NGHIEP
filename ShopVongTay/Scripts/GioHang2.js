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
                            TongTien += item.TongTien;
                        });
                        //$('#cart > a  > span').html(TongSL + " Sản Phẩm - " + TongTien + "VND")
                        $('.cart-icon > span').html(TongSL)
                        $('.cart-price > span').html(TongTien + " VNĐ")
                        alert('Đặt hàng thành công!')
                    }
                });
            return false;
        }

    };

    DatHang1 = function (MaSP) {

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
            if (parseInt($('#txtSL').val()) <= parseInt($('#sl').val())) {
                $.ajax({
                    url: "/GioHang/ThemGiohang",
                    data: { iMaSP: MaSP, SL: SL },
                    type: "POST",
                    success: function (result) {

                        var TongSL = 0;
                        var TongTien = 0;

                        $.each(result, function (i, item) {
                            TongSL += item.SoLuongMua;
                            TongTien += item.TongTien;
                        });
                        //$('#cart > a  > span').html(TongSL + " Sản Phẩm - " + TongTien + "VND")
                        $('.cart-icon > span').html(TongSL)
                        $('.cart-price > span').html(TongTien + " VNĐ")
                        alert('Đặt hàng thành công!')
                    }
                });

            }
            else {
                alert("Số lượng có hạn, vui lòng nhập lại số lượng mua!");

            }
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
                    +"<hr/>"
                    + "<p><strong>Đơn giá:</strong> " + result.price + ".VNĐ </p>"
                    + "<p><strong>Mô tả:</strong> " + result.mota + "</p>"
                    + "<p id='popupmota'><strong>Giới Tính:</strong> " + result.gioitinh + "</p>"
                    + "<p><strong>Trạng thái:</strong> " + result.trangthai + "</p>"
                    + "<button id='popupbutton' onclick='DatHang(" + result.masp + ")'>Thêm vào giỏ hàng</button> Hoặc <a href='/ViewSanPham/ChiTietSanPham?MaSP=" + result.masp + "'>Xem chi tiết</a>"
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
       var VAT = $('#magiam').text();
        var TongSL1 = 0
        $.ajax({
            url: "/GioHang/ThemGiohang",
            data: { iMaSP: MaSP, SL: SL },
            type: "POST",
            success: function (result) {
                if (result == 1) {
                    alert("quá số lượng")
                }
                else {
                    if (VAT == " ") {
                        var item = "";
                        $('.cart-table tbody').empty();
                        $.each(result, function (i, item) {
                            var R = "<tr>"
                                + "<td style='display:none;' id='MaSP' name='MaSP' class='p-price first-row'>" + item.MaSP + "</td>"
                                + "<td class='cart-pic'><img src='/img/sanpham/" + item.HinhAnh + "' /></td>"
                                + "<td class='cart-title'>" + item.TenSP + "</td>"
                                + "<td class='p-price'>" + item.DonGia + "</td>"
                                + "<td class='qua-col'><div class='quantity'><div class='pro-qty'><input type='number' min='1' max='"+ item.SoLuong +"'  id='" + item.MaSP + "' value=" + item.SoLuongMua + " onkeypress='return (event.charCode == 8 || event.charCode == 0) ? null : event.charCode >= 48 && event.charCode <= 57' required /></div></div></td>"
                                + "<td><a class='primary-btnn' onclick='CapNhat(" + item.MaSP + ")'>Cập Nhật</a></td>"
                                + "<td class='total-price'>" + item.TongTien + "</td>"
                                + "<td class='close-td'><i class='ti-close' onclick='XoaSP(" + item.MaSP + ")'></i></td >"
                                + "</tr>"
                            TongTien1 += item.TongTien;
                            TongSL1 += item.SoLuongMua;
                            $('.cart-table tbody').append(R);
                        });
                        //TongTien1 = TongTien1 - TongTien1 * VAT / 100;
                        //var tong = numeral(TongTien1).format('0,0');

                        //$('#cart > a  > span').html(TongSL1 + " Sản Phẩm - " + TongTien1 + "VND")
                        //$('.rightz > span').html("<p style='color:red; display:inline-block;padding:0;margin:0;'>" + TongSL1 + "</p> Sản phẩm - <p style='color:red;display:inline-block;padding:0;margin:0;'>" + TongTien1 + "</p> VNĐ")
                        $('.cart-icon > span').html(TongSL1)
                        $('.cart-price > span').html(TongTien1 + " VNĐ")
                        $('#tongtien').html(TongTien1)
                        $('.TongTien > h2 > span').html(TongTien1)
                    }
                    else {

                        var item = "";
                        $('.cart-table tbody').empty();
                        $.each(result, function (i, item) {
                            var R = "<tr>"
                                + "<td style='display:none;' id='MaSP' name='MaSP' class='p-price first-row'>" + item.MaSP + "</td>"
                                + "<td class='cart-pic'><img src='/img/sanpham/" + item.HinhAnh + "' /></td>"
                                + "<td class='cart-title'>" + item.TenSP + "</td>"
                                + "<td class='p-price'>" + item.DonGia + "</td>"
                                + "<td class='qua-col'><div class='quantity'><div class='pro-qty'><input type='number' min='1' max='" + item.SoLuong +"' id='" + item.MaSP + "' value=" + item.SoLuongMua + " onkeypress='return (event.charCode == 8 || event.charCode == 0) ? null : event.charCode >= 48 && event.charCode <= 57' required /></div></div></td>"
                                + "<td><a class='primary-btnn' onclick='CapNhat(" + item.MaSP + ")'>Cập Nhật</a></td>"
                                + "<td class='total-price'>" + item.TongTien + "</td>"
                                + "<td class='close-td'><i class='ti-close' onclick='XoaSP(" + item.MaSP + ")'></i></td >"
                                + "</tr>"
                            TongTien1 += item.TongTien;
                            TongSL1 += item.SoLuongMua;
                            $('.cart-table tbody').append(R);
                        });
                       
                        //var tong = numeral(TongTien1).format('0,0');

                        //$('#cart > a  > span').html(TongSL1 + " Sản Phẩm - " + TongTien1 + "VND")
                        //$('.rightz > span').html("<p style='color:red; display:inline-block;padding:0;margin:0;'>" + TongSL1 + "</p> Sản phẩm - <p style='color:red;display:inline-block;padding:0;margin:0;'>" + TongTien1 + "</p> VNĐ")
                        $('.cart-icon > span').html(TongSL1)
                        $('.cart-price > span').html(TongTien1 + " VNĐ")
                        $('#tongtien').html(TongTien1)
                        $('.TongTien > h2 > span').html(TongTien1)
                    }


                }
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
                $.each(result, function (i, item){
                    var R = "<tr>"
                        + "<td style='display:none;' id='MaSP' name='MaSP' class='p-price first-row'>" + item.MaSP + "</td>"
                        + "<td class='cart-pic'><img src='/img/sanpham/" + item.HinhAnh + "' /></td>"
                        + "<td class='cart-title'>" + item.TenSP + "</td>"
                        + "<td class='p-price'>" + item.DonGia + "</td>"

                        + "<td class='qua-col'><div class='quantity'><div class='pro-qty'><input type='number' id='" + item.MaSP + "' value=" + item.SoLuongMua + " /></div></div></td>"
                        + "<td><a class='primary-btnn' onclick='CapNhat(" + item.MaSP + ")'>Cập Nhật</a></td>"
                        + "<td class='total-price'>" + item.TongTien + "</td>"
                        + "<td class='close-td'><i class='ti-close' onclick='XoaSP(" + item.MaSP + ")'></i></td >"
                        + "</tr>"
                    TongTien += item.TongTien
                    TongSL2 += item.SoLuongMua
                    $('.cart-table tbody').append(R);

                });
               
                //$('.rightz > span').html("<p style='color:red; display:inline-block;padding:0;margin:0;'>" + TongSL2 + "</p> Sản phẩm - <p style='color:red;display:inline-block;padding:0;margin:0;'>" + TongTien + "</p> VNĐ")
                $('.cart-icon > span').html(TongSL2)
                $('.cart-price > span').html(TongTien + " VNĐ")
                $('#tongtien').html(TongTien)
                $('.TongTien > h2 > span').html(TongTien)
            },
        });
        return false;
    });
    tai = function () {
        alert("Load lại")
    };
});
