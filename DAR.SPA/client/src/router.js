import Router from "vue-router";
import Vue from "vue";

import Home from "./components/Home.vue";
import HML from "./components/HML.vue";

const routes = [
  { path: "/", component: Home },
  { path: "/hml", component: HML }
];

Vue.use(Router);

export default new Router({ mode: "history", routes });
