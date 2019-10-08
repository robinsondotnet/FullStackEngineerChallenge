import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import axios from "axios";

import FeedbackAssignmentMultiSelect from "./components/feedback/FeedbackAssignmentMultiSelect.vue";

// TODO: Move to its own file
Vue.component(
  "feedback-assignment-multi-select",
  FeedbackAssignmentMultiSelect
);

axios.defaults.baseURL = "http://localhost:3000";

Vue.config.productionTip = false;

Vue.prototype.$eventBus = new Vue();

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
