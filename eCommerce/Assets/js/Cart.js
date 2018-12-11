
﻿var CartController = CartController || {};

CartController = {
    init: function () {
        // jquery call event
        $(".add-to-cart-btn").off("click").on("click", CartController.events.addToCart);
        $(".cart-list").on("click", ".delete", CartController.events.removeFromCart);
        $("#modalremove").off("click").on("click", ".pull-right", CartController.events.removeFromCart);
    },
    events: {
        addToCart: function (event) {
            event.preventDefault();

            $.ajax({
                url: "/Cart/AddToCart",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({ Id: Number($(this).attr("data-id")), Quantity: Number($(this).attr("data-qty")) }),
                cache: false,
                success: function (result) {
                    // Change element of header Cart
                    if (result.Result == true)
                    {
                        if (result.Data != null) {
                            if ($(".product-" + result.Data.Id).length > 0)
                            {
                                $(".product-" + result.Data.Id).html("<div class='product-widget product-" + result.Data.Id + "'>"
                                    + "<div class=\"product-img\">"
                                    + "<img src='" + result.Data.Image + "' alt=\"\">"
                                    + "</div>"
                                    + "<div class=\"product-body\">"
                                    + "<h3 class=\"product-name\"><a href='" + result.Data.Url + "'>product name goes here</a></h3>"
                                    + "<h4 class=\"product-price\"><span class=\"qty\">" + result.Data.Quantity + "x" + "</span>$" + result.Data.Price + "</h4>"
                                    + "</div>"
                                    + "<button class=\"delete\"   data-id=" + result.Data.Id + "><i class=\"fa fa-close\"></i></button>"
                                    + "</div>"
                                );
                            }
                            else
                            {
                                $(".cart-list").append("<div class='product-widget product-" + result.Data.Id + "'>"
                                    + "<div class=\"product-img\">"
                                    + "<img src='" + result.Data.Image + "' alt=\"\">"
                                    + "</div>"
                                    + "<div class=\"product-body\">"
                                    + "<h3 class=\"product-name\"><a href='" + result.Data.Url + "'>product name goes here</a></h3>"
                                    + "<h4 class=\"product-price\"><span class=\"qty\">" + result.Data.Quantity + "x" + "</span>$" + result.Data.Price + "</h4>"
                                    + "</div>"
                                    + "<button class=\"delete\"   data-id=" + result.Data.Id + "><i class=\"fa fa-close\"></i></button>"
                                    + "</div>"
                                );
                            }
                            $(".cart-summary small").html(result.Data.TotalQuantity + " Item(s) selected");
                            $(".cart-summary h5").html("SUBTOTAL: $" + result.Data.TotalAmount);
                            $(".dropdown-toggle .qty").html(result.Data.TotalQuantity);
                        }
                        else {
                            alert("Có lỗi xảy ra!");
                        }

                        alert(result.Message);
                    }
                    else {
                        alert(result.Message);
                    }
                }
            });
            return false;
        },

        addOneMore: function (event) {
            event.preventDefault();

            $.ajax({
                url: "/Cart/UpdateToCart",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({ Id: "", Quantity: "" }),
                cache: false,
                success: function (result) {
                    // Change element of header Cart
                    if (result.Result == true) {
                        if (result.Data != null) {
                            $(".product-" + result.Data.Id).html("<div class='product-widget product-" + result.Data.Id + "'>"
                                + "<div class=\"product-img\">"
                                + "<img src='" + result.Data.Image + "' alt=\"\">"
                                + "</div>"
                                + "<div class=\"product-body\">"
                                + "<h3 class=\"product-name\"><a href='" + result.Data.Url + "'>product name goes here</a></h3>"
                                + "<h4 class=\"product-price\"><span class=\"qty\">" + result.Data.Quantity + "x</span>$" + result.Data.Price + "</h4>"
                                + "</div>"
                                + "<button class=\"delete\"   data-id=" + result.Data.Id + "><i class=\"fa fa-close\"></i></button>"
                                + "</div>"
                            );

                            $(".cart-summary small").html(result.Data.TotalQuantity + " Item(s) selected");
                            $(".cart-summary h5").html("SUBTOTAL: $" + result.Data.TotalAmount);
                            $(".dropdown-toggle .qty").html(result.Data.TotalQuantity);
                        }
                        else {
                            alert("Có lỗi xảy ra!");
                        }

                        alert(result.Message);
                    }
                    else {
                        alert(result.Message);
                    }
                }
            });
        },

        minusOneMore: function (event) {
            event.preventDefault();

            $.ajax({
                url: "/Cart/UpdateToCart",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({ Id: "", Quantity: "" }),
                cache: false,
                success: function (result) {
                    // Change element of header Cart
                    if (result.Result == true) {
                        if (result.Data != null) {
                            $(".product-" + result.Data.Id).html("<div class='product-widget product-" + result.Data.Id + "'>"
                                + "<div class=\"product-img\">"
                                + "<img src='" + result.Data.Image + "' alt=\"\">"
                                + "</div>"
                                + "<div class=\"product-body\">"
                                + "<h3 class=\"product-name\"><a href='" + result.Data.Url + "'>product name goes here</a></h3>"
                                + "<h4 class=\"product-price\"><span class=\"qty\">" + result.Data.Quantity + "x" + "</span>$" + result.Data.Price + "</h4>"
                                + "</div>"
                                + "<button class=\"delete\" data-id=" + result.Data.Id + "><i class=\"fa fa-close\"></i></button>"
                                + "</div>"
                            );

                            $(".cart-summary small").html(result.Data.TotalQuantity + " Item(s) selected");
                            $(".cart-summary h5").html("SUBTOTAL: $" + result.Data.TotalAmount);
                            $(".dropdown-toggle .qty").html(result.Data.TotalQuantity);
                        }
                        else {
                            alert("Có lỗi xảy ra!");
                        }

                        alert(result.Message);
                    }
                    else {
                        alert(result.Message);
                    }
                }
            });
        },

        removeFromCart: function (event) {
            event.preventDefault();

            $.ajax({
                url: "/Cart/RemoveToCart",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({ Id: Number($(this).attr("data-id")) }),
                cache: false,
                success: function (result) {
                    // Change element of header Cart
                    if (result.Result == true) {
                        if (result.Data != null) {
                            $(".product-" + result.Data.Id).remove();
                            $(".cart-summary small").html(result.Data.TotalQuantity + " Item(s) selected");
                            $(".cart-summary h5").html("SUBTOTAL: $" + result.Data.TotalAmount);
                            $(".dropdown-toggle .qty").html(result.Data.TotalQuantity);

                            if ($(".cart-product-" + result.Data.Id).length > 0)
                            {
                                $(".cart-product-" + result.Data.Id).remove();
                            }
                        }
                        else {
                            alert("Có lỗi xảy ra!");
                        }

                        alert(result.Message);
                    }
                    else {
                        alert(result.Message);
                    }
                }
            });
        }
    }
}

$(document).ready(function () {
    CartController.init();
});