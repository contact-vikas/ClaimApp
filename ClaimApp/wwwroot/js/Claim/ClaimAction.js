var base_url = "https://localhost:7247/api/";
$(document).ready(function () {
    GelAllPendingClaims();
})

function GelAllPendingClaims() {
    var data = {
        "userid": localStorage.getItem("UserRid"),
        "role":$("#hdnRole").val()
    }
    $.ajax({
        "url": base_url + "Claim/GetPendingClaims",
        "method": "get",
        contentType: JSON,
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
         "data": data,
        "success": function (response) {
            console.log(response);
            if (response.ok) {
                $("#tblPendingRequest").DataTable({
                    data: response.data,
                    columns: [
                        { "data": "claim_Title" },
                        { "data": "claim_Reason" },
                        { "data": "claimDt" },
                        { "data": "amount" },
                        { "data": "expenseDt" },
                        { "data": "nm" },
                        { "data": "claim_Description" },
                        {
                            "data": "evidence",
                            render: function (evidence) {
                                var btn = '<a class="btn btn-sm btn-info" onclick=DownloadFile("' + evidence + '")>View</a>'
                                return btn
                            }
                        },
                        {
                            "data": "id",
                            render: function (id) {
                                var btn = '<a class="btn btn-sm btn-info" onclick="ActionRequest(' + id + ')">Action</a>'
                                return btn
                            }
                        }
                        
                    ]
                })
            }
            else {
                $("#msg").html(response.message).css("color", "red")
            }
        },
        "error": function (err) {
            console.log(err)
        }
    })
}