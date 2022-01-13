const buildUrl = "../../Build";
const loaderUrl = buildUrl + "/{{{ LOADER_FILENAME }}}";
let config = {
  dataUrl: buildUrl + "/{{{ DATA_FILENAME }}}",
  frameworkUrl: buildUrl + "/{{{ FRAMEWORK_FILENAME }}}",
  codeUrl: buildUrl + "/{{{ CODE_FILENAME }}}",
#if MEMORY_FILENAME
  memoryUrl: buildUrl + "/{{{ MEMORY_FILENAME }}}",
#endif
#if SYMBOLS_FILENAME
  symbolsUrl: buildUrl + "/{{{ SYMBOLS_FILENAME }}}",
#endif
  companyName: "{{{ COMPANY_NAME }}}",
  productName: "{{{ PRODUCT_NAME }}}",
  productVersion: "{{{ PRODUCT_VERSION }}}",
};

let container = document.querySelector("#unity-container");
let canvas = document.querySelector("#unity-canvas");
let loading = document.querySelector("#unity-loading-bar");
let fullscreenButton = document.querySelector("#unity-fullscreen-button");
let mobileWarning = document.querySelector("#unity-mobile-warning");

if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
  container.className = "unity-mobile";
  config.devicePixelRatio = 1;
  mobileWarning.style.display = "block";
  setTimeout(() => {
    mobileWarning.style.display = "none";
  }, 2500);
}

loading.style.display = "none";
fullscreenButton.style.display = "none";

let script = document.createElement("script");
script.src = loaderUrl;
script.onload = () => {
  createUnityInstance(canvas, config, () => {
    loading.style.display = "flex";
  }).then((unityInstance) => {
    // if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
    //   unityInstance.SendMessage("DeviceController", "IsMobile", 1)
    // }
    loading.style.display = "none";
    fullscreenButton.style.display = "block";
    fullscreenButton.onclick = () => {
      ("use strict");

      fullscreenButton.classList.toggle("min-icon");

      if (document.fullscreenElement) {
        document.exitFullscreen();
      } else {
        document.documentElement.requestFullscreen();
      }
    };
  }).catch((message) => {
    alert(message);
  });
};

document.body.appendChild(script);
