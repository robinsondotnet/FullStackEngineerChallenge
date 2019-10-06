<template>
  <span :class="$options.name">
    <span :class="[`${$options.name}__text`, editClass]" @click="toggle">{{
      value ? value.length : 0
    }}</span>
    <component
      v-show="isVisible"
      :class="`${$options.name}__multiSelectWrapper`"
      :key="$vnode.key"
      v-model="value"
      :is="component"
      @on-close="onClose"
    />
  </span>
</template>

<script>
export default {
  name: "BaseMultiSelect",

  model: {
    prop: "model",
    event: "change"
  },

  watch: {
    model(val) {
      this.$emit("change", val);
      this.value = val;
    }
  },

  props: {
    component: String,
    editMode: {
      type: Boolean,
      required: true,
      default() {
        return false;
      }
    },
    model: {
      type: Array,
      default() {
        return [];
      }
    }
  },

  computed: {
    editClass() {
      return this.editMode ? `${this.$options.name}__text--edit` : "";
    }
  },

  methods: {
    toggle() {
      this.isVisible = !this.isVisible && this.editMode;
    },
    onClose() {
      this.isVisible = false;
    }
  },

  data() {
    return {
      value: [],
      isVisible: false
    };
  },

  mounted() {
    this.value = this.model;
  }
};
</script>

<style lang="scss">
@import "@/scss/abstracts/_colors.scss";

.BaseMultiSelect {
  &__text--edit {
    &:hover {
      font-size: 1.2em;
      font-weight: bold;
    }
  }

  &__multiSelectWrapper {
    position: absolute;
    background: $secondary-color;
    width: 150px;
    border: 0.05px solid gray;
  }
}
</style>
