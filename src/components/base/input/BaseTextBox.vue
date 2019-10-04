<template>
  <span v-if="editMode">
    <input type="text" v-model="dirty" />
  </span>
  <span v-else>
    {{ text }}
  </span>
</template>

<script>
export default {
  name: "BaseTextBox",

  props: {
    editMode: {
      type: Boolean,
      default() {
        return false;
      }
    },
    text: String,
  },
  data() {
    return {
      dirty: ""
    }
  },

  mounted() {
    this.dirty = this.text;
  }
};
</script>
