import actions from "./actions";
import getters from "./getters";
import mutations from "./mutations";

const state = {
  avatarUrl: "/gundam.jpg",
  name: "Kento"
};

export default {
  namespaced: true,
  state,
  actions,
  getters,
  mutations
};
