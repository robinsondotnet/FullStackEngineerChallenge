<template>
  <div :class="$options.name">
    <select v-model="unSavedValue" multiple>
      <option
        :key="employee.id"
        v-for="employee in employees"
        :value="employee.id"
      >
        {{ `${employee.firstName} ${employee.lastName}` }}</option
      >
    </select>

    <button @click="onSave">Assign</button>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "FeedbackAssignmentMultiSelect",

  computed: {
    ...mapState({
      employees: state => state.employee.employees
    })
  },

  prop: {
    value: {
      type: Array,
      default() {
        return [];
      }
    },
    key: {
      type: [String, Number],
      required: true
    }
  },

  watch: {
    value(val) {
      this.unSavedValue = val;
    }
  },

  methods: {
    onSave() {
      const employeeId = this.$vnode.key.split("-")[0];
      this.$eventBus.$emit("ON_REVIEW_FEEDBACK_ASSIGNED", {
        employeeId,
        feedbackAssigneeEmployees: this.unSavedValue
      });
      this.$emit("on-close");
    }
  },

  data() {
    return {
      unSavedValue: []
    };
  }
};
</script>

<style lang="scss">
.FeedbackAssignmentMultiSelect {
  display: flex;
  flex-direction: column;
}
</style>
