/*================================================================================
	Item Name: Materialize - Material Design Admin Template
	Version: 5.0
	Author: PIXINVENT
	Author URL: https://themeforest.net/user/pixinvent/portfolio
================================================================================

NOTE:
------
PLACE HERE YOUR OWN JS CODES AND IF NEEDED.
WE WILL RELEASE FUTURE UPDATES SO IN ORDER TO NOT OVERWRITE YOUR CUSTOM SCRIPT IT'S BETTER LIKE THIS. */

$(document).ready(function() {
    $(".notification-button").dropdown({
        hover: true,
    coverTrigger: false,
    closeOnClick: false
  });
  function formatMoney(
    amount,
    decimalCount = 2,
    decimal = ".",
    thousands = ","
  ) {
    try {
      decimalCount = Math.abs(decimalCount);
      decimalCount = isNaN(decimalCount) ? 2 : decimalCount;

      const negativeSign = amount < 0 ? "-" : "";

      let i = parseInt(
        (amount = Math.abs(Number(amount) || 0).toFixed(decimalCount))
      ).toString();
      let j = i.length > 3 ? i.length % 3 : 0;

      return (
        negativeSign +
        (j ? i.substr(0, j) + thousands : "") +
        i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousands)
      );
    } catch (e) {
      console.log(e);
    }
  }

  //init modal trigger open
  $(".modal").modal();

  //Dropdown
  $(".table .dropdown-trigger").dropdown({
    inDuration: 350,
    outDuration: 225,
    constrainWidth: false, // Does not change width of dropdown to that of the activator
    hover: false, // Activate on hover
    gutter: 0, // Spacing from edge
    coverTrigger: false, // Displays dropdown below the button
    alignment: "left", // Displays dropdown with edge aligned to the left of button
    stopPropagation: false // Stops event propagation
  });
  // ===== Scroll to Top ====
  $(window).scroll(function() {
    if ($(this).scrollTop() >= 50) {
      // If page is scrolled more than 50px
      $("#return-to-top").fadeIn(200); // Fade in the arrow
    } else {
      $("#return-to-top").fadeOut(200); // Else fade out the arrow
    }
  });
  $("#return-to-top").click(function() {
    // When arrow is clicked
    $("body,html").animate(
      {
        scrollTop: 0 // Scroll to top of body
      },
      500
    );
  });
  //lightbox
  $(".materialboxed").materialbox();
  //filter toggle
    $("#filter-btn").on("click", function () {
        $(this).toggleClass("active");
        $(this).closest(".row").find('.filter-wrap')
            .slideToggle();
    });
    $.datetimepicker.setLocale('vi');
    $('.datetimepicker').each(function () {
        $(this).attr('autocomplete', 'off');
        $(this).datetimepicker({
            format: 'd/m/Y H:00',
            formatDate: 'd/m/Y',
            allowTimes: [
                '00:00',
                '01:00',
                '02:00',
                '03:00',
                '04:00',
                '05:00',
                '06:00',
                '07:00',
                '08:00',
                '09:00',
                '10:00',
                '11:00',
                '12:00',
                '13:00',
                '14:00',
                '15:00',
                '16:00',
                '17:00',
                '18:00',
                '19:00',
                '20:00',
                '21:00',
                '22:00',
                '23:00',
            ],
            // theme:'dark',
            // defaultDate:new Date(),
            onShow: function (ct, element) {
                if ($(element).hasClass('to-date')) {
                    var minDate = $(element).closest('.row').find('.from-date').val();
                    console.log(minDate);
                    this.setOptions({
                        minDate: minDate ? minDate : false,
                    })
                }
                if ($(element).hasClass('date-only')) {
                    this.setOptions({
                        timepicker: false
                    })
                }
            }
        });
    })
    $("body").on("input", '[data-type="number"]', function () {
        var value = $(this)
            .val()
            .replace(/[^0-9]/, "");
        $(this).val(value);
    });
    //add attribute data-type="currency" for input to set input type currency format
    var inputMoney = document.querySelectorAll('[data-type="currency"]');
    function inputNumberTest() {
        var regx = /\D+/g;
        var number = this.value.replace(regx, "");
        this.setAttribute('origin-money', number);
        return (this.value = number.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,"));
    }
    inputMoney.forEach(function (input, index) {
        input.addEventListener("input", inputNumberTest);
    });
  //Select price
  $(".from-price").on("change", function() {
    console.log($(this).val());
    $(".to-price").attr("min", $(this).val());
  });

  //select all dropdown
  $("select").formSelect();
  $("select.select_all")
    .siblings("ul")
    .prepend("<li id=sm_select_all><span>Chọn tất cả</span></li>");
  $("li#sm_select_all").on("click", function() {
    var jq_elem = $(this),
      jq_elem_span = jq_elem.find("span"),
      select_all = jq_elem_span.text() == "Chọn tất cả",
      set_text = select_all ? "Bỏ chọn tất cả" : "Chọn tất cả";
    jq_elem_span.text(set_text);
    jq_elem
      .siblings("li")
      .filter(function() {
        return (
          $(this)
            .find("input")
            .prop("checked") != select_all
        );
      })
      .click();
  });

  //Mask input type
  $('[data-type="phone-number"]').formatter({
    pattern: "{{99999999999}}"
  });
  $('[data-type="dateofbirth"]').formatter({
    pattern: "{{99}}-{{99}}-{{9999}}"
  });

  //click upload file
  $("body").on("click", ".btn-upload", function(e) {
    e.preventDefault();
    $(this)
      .siblings('input[type="file"]')
      .trigger("click");
  });

  //Print btn
  $(".btn-print").each(function() {
    $(this).on("click", function() {
      window.focus();
      window.print();
      window.close();
    });
  });
  //Edit mode
  $("body").on("click", ".edit-mode", function(e) {
    e.preventDefault();
    $("body").addClass("edit-mode-open");
    $(".detail-fixed").animate({ scrollTop: 0 }, "fast");
  });
  $("body").on("click", ".close-editmode", function(e) {
    e.preventDefault();
    $("body").removeClass("edit-mode-open");
  });
  $(document).click(function(event) {
    //if you click on anything except the modal itself or the "open modal" link, close the modal

    if (
      !$(event.target).closest(
        ".edit-mode,.detail-fixed,.detail-fixed::-webkit-scrollbar"
      ).length
    ) {
      $("body").removeClass("edit-mode-open");
    }
  });

  // Table type design
  // if ($(".fixed-header-tb").length) {
  //   $(".fixed-header-tb").DataTable({
  //     responsive: false,
  //     scrollY: "60vh",
  //     scrollCollapse: true,
  //     paging: true,
  //     ordering: true,
  //     info: true,
  //     language: {
  //       searchPlaceholder: "Tìm kiếm",
  //       sLengthMenu: "Hiển thị _MENU_ kết quả tìm kiếm"
  //     }
  //   });
  // }

    $('body').on('focus', 'input:not(.select2-search__field),select,textarea', function () {
        $(this).css('outline', '2px solid #ED462F');
    });
    $('body').on('blur', 'input,select,textarea', function () {
        $(this).attr('style', '');
    });


    function changeLayout(x) {
        if (x.matches) { // If media query matches
            $('.sidenav-main').removeClass('nav-collapsed').addClass('nav-lock');
            $('.navbar-toggler .material-icons').text('radio_button_checked');
            $('#main').removeClass('main-full');
        } else {
            $('.sidenav-main').addClass('nav-collapsed');
            $('.navbar-toggler .material-icons').text('radio_button_unchecked');
            $('#main').addClass('main-full');
        }
    }

    var media = window.matchMedia("(min-width: 1920px)")
    changeLayout(media) // Call listener function at run time
    media.addListener(changeLayout) // Attach listener function on state changes
});

window.addEventListener("DOMContentLoaded", function() {
  //Drag tabble
  var elementScroll = document.querySelectorAll(".responsive-tb");

  if (elementScroll != undefined || elementScroll != null) {
    elementScroll.forEach(function(element) {
      var mx = 0;
      element.addEventListener("mousedown", function(e) {
        this.sx = this.scrollLeft;
        mx = e.pageX - this.offsetLeft;

        this.addEventListener("mousemove", mouseMoveFunction);
      });
      element.addEventListener("mouseup", function(e) {
        this.removeEventListener("mousemove", mouseMoveFunction);
        mx = 0;
      });
      function mouseMoveFunction(e) {
        var mx2 = e.pageX - this.offsetLeft;
        if (mx) this.scrollLeft = this.sx + mx - mx2;
      }
    });
  }

  //resize table
  var tables = document.querySelectorAll(".list-package table");
  for (var i = 0; i < tables.length; i++) {
    resizableGrid(tables[i]);
  }

  function resizableGrid(table) {
    var row = table.getElementsByTagName("tr")[0],
      cols = row ? row.children : undefined;
    if (!cols) return;

    table.style.overflow = "hidden";

    var tableHeight = table.offsetHeight;

    for (var i = 0; i < cols.length; i++) {
      var div = createDiv(tableHeight);
      cols[i].appendChild(div);
      cols[i].style.position = "relative";
      setListeners(div);
    }

    function setListeners(div) {
      var pageX, curCol, nxtCol, curColWidth, nxtColWidth;

      div.addEventListener("mousedown", function(e) {
        curCol = e.target.parentElement;
        nxtCol = curCol.nextElementSibling;
        pageX = e.pageX;

        var padding = paddingDiff(curCol);

        curColWidth = curCol.offsetWidth - padding;
        if (nxtCol) nxtColWidth = nxtCol.offsetWidth - padding;
      });

      div.addEventListener("mouseover", function(e) {
        e.target.style.borderRight = "1px solid #000";
      });

      div.addEventListener("mouseout", function(e) {
        e.target.style.borderRight = "";
      });

      document.addEventListener("mousemove", function(e) {
        if (curCol) {
          var diffX = e.pageX - pageX;

          if (nxtCol) nxtCol.style.width = nxtColWidth - diffX + "px";

          curCol.style.width = curColWidth + diffX + "px";
        }
      });

      document.addEventListener("mouseup", function(e) {
        curCol = undefined;
        nxtCol = undefined;
        pageX = undefined;
        nxtColWidth = undefined;
        curColWidth = undefined;
      });
    }

    function createDiv(height) {
      var div = document.createElement("div");
      div.style.top = 0;
      div.style.right = 0;
      div.style.width = "5px";
      div.style.position = "absolute";
      div.style.cursor = "col-resize";
      div.style.userSelect = "none";
      div.style.height = height + "px";
      return div;
    }

    function paddingDiff(col) {
      if (getStyleVal(col, "box-sizing") == "border-box") {
        return 0;
      }

      var padLeft = getStyleVal(col, "padding-left");
      var padRight = getStyleVal(col, "padding-right");
      return parseInt(padLeft) + parseInt(padRight);
    }

    function getStyleVal(elm, css) {
      return window.getComputedStyle(elm, null).getPropertyValue(css);
    }
  }
});

//Upload image preview
function previewFiles(e) {
  var preview = e.parentNode.nextElementSibling;
  var files = e.files;
  function readAndPreview(file) {
    // Make sure `file.name` matches our extensions criteria
    if (/\.(jpe?g|png|gif)$/i.test(file.name)) {
      var reader = new FileReader();

      reader.addEventListener(
        "load",
        function() {
          var image = new Image();
          image.height = 50;
          image.title = file.name;
          image.src = this.result;
          image.classList = "materialboxed";
          var wrap = document.createElement("div");
          wrap.classList = "img-block";
          wrap.appendChild(image);
          var close = document.createElement("span");
          close.classList = "material-icons red-text delete";
          close.innerHTML = "clear";
          close.onclick = function() {
            this.parentNode.parentNode.removeChild(this.parentNode);
          };
          wrap.appendChild(close);
          preview.appendChild(wrap);
        },
        false
      );

      reader.readAsDataURL(file);
    }
  }

  if (files) {
    var promise = new Promise(function(resolve, reject) {
      resolve([].forEach.call(files, readAndPreview));
    });
    promise.then(function() {
      setTimeout(function() {
        $(".materialboxed").materialbox();
      }, 1000);
    });
  }
}
