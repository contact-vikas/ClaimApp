var base_url = "https://localhost:7247/api/";

$(document).ready(function () {
    $("#btnSubmit").click(function () {
        console.log("I am clicked")
        addClaimRequest()
    })

    $("#btnReload").click(function () {
        window.location.reload()
    })
})


function addClaimRequest() {
    var isValid = true;
    if (isValid) {
        var data = new FormData();
        data.append("ClaimTitle", $("#txtClaimTitle").val());
        data.append("ClaimReason", $("#txtClaimReason").val());
        data.append("ClaimDescription", $("#txtClaimDescription").val());
        data.append("ClaimAmount", $("#txtClaimAmount").val());
        data.append("ExpenseDt", $("#txtClaimExpenseDt").val());
        data.append("UserId", localStorage.getItem("UserRid"));
        data.append("file", $("#txtClaimEvidence")[0].files[0]);

        $.ajax({
            "url": base_url+"Claim/RaiseClaim",
            "method": "post",
            cache: false,
            contentType: false,
            processData: false,
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
            "data": data,
            "success": function (response) {
                if (response.ok) {
                    $("#msg").html(response.message).css("color", "green");
                    setTimeout(function () {
                        location.reload();
                    }, 1000)
                }
                else {
                    $("#msg").html(response.message).css("color", "red");
                }
            },
            
            "error": function (err) {
                console.log(err)
            }
        })
    }

}