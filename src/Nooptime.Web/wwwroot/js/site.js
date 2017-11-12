﻿document.addEventListener("DOMContentLoaded",
    function (event) {


        var vm = new Vue(
            {
                el: "#vue-instance",
                data: {
                    items: []
                },
                created: function () {
                    var that = this;

                    axios.get("/api/UptimeCheck/GetAll")
                        .then(function (response) {

                            // Replace the data
                            // See https://vuejs.org/v2/guide/list.html#Replacing-an-Array
                            that.items = that.items.concat(response.data);
                        })
                        .catch(function (error) {
                            console.log(error);
                        });
                },
                methods: {
                    add: function () {
                        var that = this;

                        if (document.getElementsByTagName("form")[0].checkValidity()) {
                            var model = {
                                Id: "00000000-0000-0000-0000-000000000000",
                                Name: document.getElementById("name").value,
                                Description: document.getElementById("description").value,
                                Interval: document.getElementById("interval").value
                            };

                            axios.post("/api/UptimeCheck/Post", model)
                                .then(function (response) {
                                    that.items.push(model);
                                    console.log(response);
                                })
                                .catch(function (error) {
                                    console.log(error);
                                });
                        }
                    },

                    remove: function (item, index) {
                        var that = this;

                        axios.delete("/api/UptimeCheck/Delete", { params: { id: item.id } })
                            .then(function (response) {
                                that.items.splice(index, 1);
                                console.log(response);
                            })
                            .catch(function (error) {
                                console.log(error);

                            });
                    }
                }
            });
    });
