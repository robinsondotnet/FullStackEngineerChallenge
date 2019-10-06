import actions from "./actions";
import getters from "./getters";
import mutations from "./mutations";

const state = {
  menuOptions: [
    { id: 1, link: "/employee", text: "Employees", icon: "store", childs: [] },
    //{ id: 2, link: "/reviews", text: "Reviews", icon: "box-open", childs: [] }
  ]
};

export default {
  namespaced: true,
  state,
  actions,
  getters,
  mutations
};
