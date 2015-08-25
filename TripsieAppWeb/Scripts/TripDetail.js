
        $(document).ready(function () {
            $('#UpdateLabel').hide();
            $('#ResponseDiv').hide();
            $('#ReponseIndicator').hide();


            $('#RB-In').click(function () {
                $('#RB-Out').prop('checked', false);

                $('#ResponseDiv').fadeIn(200);
            });

            $('#RB-Out').click(function () {
                $('#RB-In').prop('checked', false);

                $('#ResponseDiv').fadeIn(200);
            });

            $('#ButtonResponse').click(function () {
                $('#ButtonResponse').prop('enabled', false);
                $('#ReponseIndicator').show();

                var tripUserId = $('#TripUserCode').val();
                var comment = $('#ResponseTextArea').val();
                var response = $('#RB-In').prop('checked');

                if (comment == "") {
                    comment = "-1111";
                }

                var server = "http://teambitewolf.azurewebsites.net";
                //server = "http://localhost:49669"; 
                var myUrl = server + "/api/TripUsers/Response/" + tripUserId + "/" + response + "/" + comment;

                var success = false;

                $.ajax({
                    dataType: "text",
                    url: myUrl,
                    type: 'PUT',
                    success: function (data) {
                        success = true;
                        $('#UpdateLabel').text("Updated your response!");
                        $('#UpdateLabel').show();

                    },

                    error: function (x, y, z) {
                        alert("Error: unable to change your response. Please try again.");
                        var currentVal = $('#ButtonResponse').val();
                        $('#ButtonResponse').prop('enabled', !currentVal);
                    },

                    complete: function()
                    {
                        if (success) {
                            var status = 1;
                            var imgString = "/Content/Images/yes.png";

                            if (!response) {
                                status = 2;
                                imgString = "/Content/Images/no.png";
                            }

                            var image = $('#Status-' + tripUserId);
                            image.attr('src', imgString);

                            document.getElementById('Response').value = status;
                        }

                        $('#ButtonResponse').prop('enabled', true);
                        $('#ReponseIndicator').hide();
                        $('#ResponseDiv').hide();
                    }
                });
            });
        });
