<template>
  <span v-if="editMode">
    <label>
      <input :type="type" v-model="value" />
    </label>
  </span>
  <span v-else>
    {{ value }}
  </span>
</template>

<script>
export default {
  name: "BaseTextBox",

  model: {
    prop: "model",
    event: "input"
  },

  props: {
    editMode: {
      type: Boolean,
      default() {
        return false;
      }
    },
    model: [String, Number],
    type: {
      type: String,
      default() {
        return "text";
      }
    }
  },

  watch: {
    value(val) {
      if (this.type === "number") {
        this.$emit("input", Number(val));
      } else {
        this.$emit("input", val);
      }
    },
    model(val) {
      this.value = val;
    }
  },

  data() {
    return {
      value: null
    };
  }
};
</script>
