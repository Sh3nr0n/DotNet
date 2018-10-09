$(document).ready(function () {
    $('#search').on('keyup', searchFunction);
    similarFunction();
    movieFunction();
});


function searchFunction() {
    $.ajax({
        url: "/Search/Search",
        data: { search: $("#search").val() }
    })
        .done(function (result) {
            $("#searchResult").html(result);
        })
        .fail(function (jqXHR, textStatus, error) {
            M.toast({ html: error });
        });
}

function similarFunction() {
    $.ajax({
        url: "/Home/GetSimilarMovie",
        data: { id: $("#movieId").val() }
    })
        .done(function (result) {
            $("#similarResult").html(result);
        });
}

function movieFunction() {
    $.ajax({
        url: "/Home/GetVideo",
            data: { id: $("#movieId").val() }
        })
        .done(function (result) {
            $("#videoResult").html(result);
        });
}