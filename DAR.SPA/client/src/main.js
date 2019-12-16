import Vue from "vue";
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import router from "./router";
import ApiService from "./services/ApiService";
Vue.config.productionTip = false;

ApiService.init("https://localhost:5001/");
new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount("#app");
