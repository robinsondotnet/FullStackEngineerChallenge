<template>
  <component :is="innerComponent" :class="$options.name" :to="link">
    <span :if="icon" :class="`${$options.name}__icon`">
      <i :class="`fas fa-${icon}`"></i>
    </span>
    <span :class="`${$options.name}__text`">
      {{ text }}
    </span>
  </component>
</template>

<script>
export default {
  name: "BaseIconLabel",

  props: {
    link: {
      type: String,
      default: null
    },
    icon: {
      type: String,
      required: true
    },
    text: {
      type: String,
      required: true
    }
  },

  computed: {
    innerComponent() {
      if (this.link == null) {
        ("div");
      }

      return "router-link";
    }
  }
};
</script>

<style lang="scss">
@import "@/scss/abstracts/_colors.scss";

.BaseIconLevel {
  width: 100%;
  display: flex;
  color: #fff;
  justify-content: left;
  align-items: center;
  text-decoration: none;
  height: 30px;

  &:hover {
    background-color: $secondary-color;
    font-weight: bold;
  }

  &__icon {
    padding-left: 15%;
  }

  &__text {
    padding-left: 5%;
  }
}
</style>
