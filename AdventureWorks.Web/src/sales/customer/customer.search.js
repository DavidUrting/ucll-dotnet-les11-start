import $ from "jquery";

if (document.getElementById("searchButton") !== null) {
    document.getElementById("searchButton").addEventListener("click", function(e) {
        // vorige resultaten leegmaken.
        $("#customers tbody tr").remove();

        // Vorige 'fouten' leegmaken
        var keywordErrors = document.querySelector("span[data-valmsg-for='Query.Keyword']");
        keywordErrors.innerHTML = "";

        // keyword achterhalen. Indien leeg: geen query doen.
        let keyword = document.getElementById("keywordInput").value;
        if (keyword) {
            $.post("/sales/customer/search?keyword=" + keyword, function (result) {
                for (let i = 0; i < result.length; i++) {
                    $("#customers").find('tbody').append(
                        '<tr><td>' + result[i].id +
                        '</td><td>' + result[i].firstName +
                        '</td><td>' + result[i].lastName +
                        '</td><td>' + result[i].email +
                        '</td><td><a href="/sales/customer/details/' + result[i].id + '">Details</a> | <a href="/sales/customer/edit/' + result[i].id + '">Edit</a> | <a href="/sales/customer/delete/' + result[i].id + '">Delete</a></td></tr>');
                }
            }).fail(function (err) {
                if (err.responseJSON && err.responseJSON.Keyword) {
                    keywordErrors.innerHTML = err.responseJSON.Keyword[0];
                } else {
                    console.error(err);
                    alert("Er is een onverwachte fout opgetreden.");
                }
            });
;
        }
        e.preventDefault();
    });
}

