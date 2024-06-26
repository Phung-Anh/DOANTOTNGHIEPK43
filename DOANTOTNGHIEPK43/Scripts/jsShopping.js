﻿$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quatity = 1;
        var tQuantity = $('#quantity_value').text();
        if (tQuantity !== '') {
            quatity = parseInt(tQuantity);
        }
        $.ajax({
            url: '/shoppingcart/AddToCart',
            type: 'POST',
            data: { id: id, quantity: quatity },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                };
            }
        });
    })
    $('body').on('click', '.btnDeleted', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm("Bạn có muốn xóa hay không");
        if (conf == true) {
            $.ajax({
                url: '/shoppingcart/Deteled',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout_items').html(rs.Count);
                        $('#trow_' + id).remove();
                        LoadCart();

                    };
                }
            });
        }
       
    })

    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity);

    });


    $('body').on('click', '.btnDeletedAll', function (e) {
        e.preventDefault();
        var conf = confirm("Bạn có muốn xóa hết giỏ hàng hay không");
        if (conf == true) {
            DeletedAll();
        }

    })

});




function ShowCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.Count);
        }
    });
}

function DeletedAll() {
    $.ajax({
        url: '/shoppingcart/DeletedAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}
function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}
function Update(id , quantity) {
    $.ajax({
        url: '/shoppingcart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}