import Vue from "vue";
import Vuex from "vuex";
import user from "./modules/user";
import permission from "./modules/permission";
import employee from "./modules/employee";
import review from "./modules/review";
import feedback from "./modules/feedback";

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    user,
    employee,
    permission,
    review,
    feedback
  },
  state: {},
  mutations: {},
  actions: {}
});
