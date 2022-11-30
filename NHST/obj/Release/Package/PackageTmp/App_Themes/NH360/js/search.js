const product = document.querySelector('#product');
const tracking = document.querySelector('#tracking');

const productFrm = document.querySelector('#form-product');
const trackingFrm = document.querySelector('#form-tracking');

if(product && tracking && productFrm && trackingFrm) {
    product.addEventListener("click", () => {
      product.classList.add("active");
      tracking.classList.remove("active");

      productFrm.classList.remove("d-none");
      trackingFrm.classList.add("d-none");
    });
    
    tracking.addEventListener("click", () => {
      product.classList.remove("active");
      tracking.classList.add("active");

      productFrm.classList.add("d-none");
      trackingFrm.classList.remove("d-none");
    });
}
