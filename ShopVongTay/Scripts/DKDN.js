$(document).ready(function () {
    $('#sub-DangKy').click(function () {
        var emailID = document.form1.email.value;
        atpos = emailID.indexOf("@");
        dotpos = emailID.lastIndexOf(".");

        if (atpos < 1 || (dotpos - atpos < 2)) {
            alert("Vui lòng nhập Email chính xác")
            document.form1.email.focus();
            return false;
        }
        return (true);
        if ($('#hoten').val() == "" || $('#email').val() == "" || $('#tendn').val() == "" || $('#amatkhau').val() == "" ||
            $('#matkhau').val() == "" || $('#diachi').val() == "") {
            $('#hoten').css('border-color', '#ddd');
            $('#tendn').css('border-color', '#ddd');
            $('#email').css('border-color', '#ddd');
            $('#matkhau').css('border-color', '#ddd');
            $('#amatkhau').css('border-color', '#ddd');
            $('#diachi').css('border-color', '#ddd');
            if ($('#hoten').val() == "") {
                $('#hoten').css('border-color', '#ff0000');
            }
            if ($('#email').val() == "") {
                $('#email').css('border-color', '#ff0000');
            }
            if ($('#matkhau').val() == "") {
                $('#matkhau').css('border-color', '#ff0000');
            }
            if ($('#amatkhau').val() == "") {
                $('#amatkhau').css('border-color', '#ff0000');
            }
            if ($('#diachi').val() == "") {
                $('#diachi').css('border-color', '#ff0000');
            }
            if ($('#tendn').val() == "") {
                $('#tendn').css('border-color', '#ff0000');
            }
            $('#ThongBao').html("Vui lòng nhập thông tin đầy đủ!!!!!!!!!!!!")
            
            return false;
        }

    });
    $('#sub-DN').click(function () {
        if ($('#TaiKhoan').val() == "" || $('#MK').val() == "") {
            $('#TaiKhoan').css('border-color', '#ddd');
            $('#MK').css('border-color', '#ddd');
            if ($('#TaiKhoan').val() == "") {
                $('#TaiKhoan').css('border-color', "red");
            }
            if ($('#MK').val() == "") {
                $('#MK').css('border-color', "red");
            }
            return false;
        }
        else {
            $('#DN-ThongBao').css('display', 'block');
        }
    });
    /*js Sửa Thông Tin*/
    $('#btn-SuaTT').click(function () {
        $('#TT-Right').css('display', 'none');
        $('#TT-Right2').css('display', 'block');
        $('#TT-Right3').css('display', 'none');
    });
    $('#btn-LuuTT').click(function () {
        var MaKH = $('#LayMKH').val();
        var HoTen = $('#HoTen').val();
        var GioiTinh = $('#GioiTinh').val();
        var Email = $('#Email').val();
        var DiaChi = $('#DiaChi').val();
        var SDT = $('#SDT').val();
        var NgaySinh = $('#NgaySinh').val();
        $.ajax({
            url: "/DKDN/CapNhatThongTin",
            data: { HoTen: HoTen, GioiTinh: GioiTinh, Email: Email, DiaChi: DiaChi, SDT: SDT, NgaySinh: NgaySinh, MaKH: MaKH },
            type: "POST",
            success: function (result) {
                alert(result)
                $('#TT-Right').css('display', 'block');
                $('#TT-Right2').css('display', 'none');
                $('#TT-Right3').css('display', 'none');
                $('#TT-HT').html(HoTen)
                $('#TT-GT').html(GioiTinh)
                $('#TT-E').html(Email)
                $('#TT-DC').html(DiaChi)
                $('#TT-DT').html(SDT)
                $('#TT-NS').html(NgaySinh)
            }
        });
    });
    /*js Đổi MK*/
    $('#btn-DoiMK').click(function () {
        $('#TT-Right').css('display', 'none');
        $('#TT-Right2').css('display', 'none');
        $('#TT-Right3').css('display', 'block');
    });
    $('#btn-LuuTT2').click(function () {
        var MaKH = $('#LayMKH').val();
        var MKCu = $('#MKCu').val();
        var MKMoi = $('#MKMoi').val();
        var LaiMKMoi = $('#LaiMKMoi').val();
        if (MKMoi == LaiMKMoi) {
            $.ajax({
                url: '/DKDN/CapNhatMatKhau',
                type: 'POST',
                data: { MaKH: MaKH, MatKhauCu: MKCu, MatKhauMoi: MKMoi },
                success: function (result) {
                    if (result == 'false') {
                        alert('Mật Khẩu Củ Không Chính Xác');
                    }
                    else {
                        alert("Thay Đổi Mật Khẩu Thành Công")
                        $('#TT-Right').css('display', 'block');
                        $('#TT-Right2').css('display', 'none');
                        $('#TT-Right3').css('display', 'none');
                    }
                }
            });
        }
        else {
            alert("Mật Khẩu Mới Không Trùng Vui Lòng Nhập Lại!")
        }

    });
}); 