import Vue from "vue";
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import router from "./router";
import ApiService from "./services/ApiService";
Vue.config.productionTip = false;

ApiService.init("http://localhost:5000/");
new Vue({
  router,
  vuetify,
  render: h => h(App)
}).$mount("#app");
