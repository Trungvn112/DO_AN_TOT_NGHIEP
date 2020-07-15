var SL = 1;
function Tru() {
    SL = document.getElementById("txtSL").value;
    if (isNaN(SL) == true) {
        SL = 1;
    }
    SL--;
    if (SL < 1) {
        SL = 1;
    }
    document.getElementById("txtSL").value = SL;
};
function Cong() {
    SL = document.getElementById("txtSL").value;
    if (isNaN(SL) == true) {
        SL = 1;
    }
    SL++;
    document.getElementById("txtSL").value = SL;
};

$(document).ready(function () {
    $('#Tab-R').click(function () {
        if ($('.Tab-Review').is(":hidden") == true) {
            $('.Tab-Review').css("display", "block")
            $('#Tab-R').css({ "background-color": "#fff", "padding-bottom": "13px" })
            $('.Tab-Description').css("display", "none");
            $('#Tab-L').css({ "background-color": "#eee", "padding-bottom": "11px" })
        }
    });
    $('#Tab-L').click(function () {
        if ($('.Tab-Description').is(":hidden") == true) {
            $('.Tab-Description').css("display", "block")
            $('#Tab-L').css({ "background-color": "#fff", "padding-bottom": "13px" })
            $('.Tab-Review').css("display", "none");
            $('#Tab-R').css({ "background-color": "#eee", "padding-bottom": "11px" })
        }
    });

    //--------------------Ratting-------------------------
    var Rating;
    Star = function (s) {
        Rating = s
    }
    $('#subRating').click(function () {

        var MS = $('#MaSach').val();
        var Ngay = $('#Ngay').val();
        var name = $('#txt-frmRating').val();
        var BL = $('#erea-frmRating').val();
        var Rating1 = Rating;
        $.ajax(
            {
            url: "/ViewSanPham/ChiTietSanPham",
            data: { iMaSach: MS, iName: name, iComment: BL, Ngay: Ngay, iRating: Rating },
            type: "POST",
            success: function (data) {
                $('.new-Cmt .author > b').append(name);
                $('.new-Cmt .author > span').append(Ngay);
                if (Rating == 1) {
                    $('.new-Cmt .rating-review').append("<i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star' aria-hidden='true'></i><i class='fa fa-star' aria-hidden='true'></i><i class='fa fa-star ' aria-hidden='true'></i><i class='fa fa-star ' aria-hidden='true'></i>")
                }
                else if (Rating == 2) {
                    $('.new-Cmt .rating-review').append("<i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star 'aria-hidden='true'></i><i class='fa fa-star ' aria-hidden='true'></i><i class='fa fa-star ' aria-hidden='true'></i>")
                }
                else if (Rating == 3) {
                    $('.new-Cmt .rating-review').append("<i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star star'aria-hidden='true'></i><i class='fa fa-star ' aria-hidden='true'></i><i class='fa fa-star ' aria-hidden='true'></i>")
                }
                else if (Rating == 4) {
                    $('.new-Cmt .rating-review').append("<i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star star'aria-hidden='true'></i><i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star ' aria-hidden='true'></i>")
                }
                else if (Rating == 5) {
                    $('.new-Cmt .rating-review').append("<i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star star'aria-hidden='true'></i><i class='fa fa-star star' aria-hidden='true'></i><i class='fa fa-star star' aria-hidden='true'></i>")
                }
                $(".new-Cmt .text-review").append(BL)
                $(".new-Cmt").css("display", "block");
            }
        });
        return false;
    });
});