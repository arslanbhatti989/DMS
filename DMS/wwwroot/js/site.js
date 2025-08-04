// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".BuyerType").on('change', function () {
        //debugger;
        var Vals = $(this).val();
        if (Vals == "Individual")
        {
            $(this).closest('.BuyerForms').find(".company-fields").addClass("d-none");
            $(this).closest('.BuyerForms').find(".individual-fields").removeClass("d-none");
        }
        else if (Vals == "Company")
        {
            $(this).closest('.BuyerForms').find(".company-fields").removeClass("d-none");
            $(this).closest('.BuyerForms').find(".individual-fields").addClass("d-none");
        }  
    })
    $(".BuyerDynamic").each(function () {
        var vals = $(this).attr("data-value");
        $(this).val(vals);

    });
    $(".countryselect").each(function () {
        var value = $(this).attr('data-value');
        if (value != "") {
            $(this).val(value);
        }
    })
    $(".MainBuyer").on('click', function () {
        $(".MainBuyer").prop('checked', false);

        // Check only the one that was clicked
        $(this).prop('checked', true);
    });
})
