<template>
  <ul :class="$options.name">
    <li
      :class="`${$options.name}__optionsWrapper`"
      :key="menuOption.id"
      v-for="menuOption in menuOptions"
    >
      <base-icon-label
        :icon="menuOption.icon"
        :text="menuOption.text"
        :link="menuOption.link"
      />
    </li>
  </ul>
</template>

<script>
import BaseIconLabel from "@/components/base/BaseIconLabel.vue";

export default {
  name: "AppSidebarMenu",

  components: {
    BaseIconLabel
  },

  props: {
    menuOptions: Array,
  }
};
</script>

<style lang="scss">
@import "@/scss/abstracts/_mixins.scss";
@import "@/scss/abstracts/_colors.scss";

.AppSidebarMenu {
  padding-left: 0px;
  margin-top: 0px;
  margin-bottom: 0px;

  &__optionsWrapper {
    @include bg-transparent($secondary-color);
    list-style: none;
  }
}
</style>
