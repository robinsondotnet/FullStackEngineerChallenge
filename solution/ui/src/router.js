import Vue from "vue";
import Router from "vue-router";
import PageEmployees from "./pages/PageEmployees.vue";
import PageReviews from "./pages/PageReviews.vue";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "*",
      redirect: "/employee"
    },
    {
      path: "/employee",
      component: PageEmployees
    },
    {
      path: "/review",
      component: PageReviews
    }
  ]
});
