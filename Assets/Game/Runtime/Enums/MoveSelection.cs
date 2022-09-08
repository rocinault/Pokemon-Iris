namespace Iris
{
    internal enum MoveSelection
    {
        fight,
        bag,
        pokemon,
        run
    }
}

/*

        protected override void AddListeners()
        {
            m_Properties.fightButton.onClick.AddListener(OnFightButtonClicked);
            m_Properties.bagButton.onClick.AddListener(OnBagButtonClicked);
            m_Properties.pokemonButton.onClick.AddListener(OnPokemonButtonClicked);
            m_Properties.runButton.onClick.AddListener(OnRunButtonClicked);
        }

        private void OnFightButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.fight);
            EventSystem.instance.Invoke(args);
        }

        private void OnBagButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.bag);
            EventSystem.instance.Invoke(args);
        }

        private void OnPokemonButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.pokemon);
            EventSystem.instance.Invoke(args);
        }

        private void OnRunButtonClicked()
        {
            var args = MoveButtonClickedEventArgs.CreateEventArgs(MoveSelection.run);
            EventSystem.instance.Invoke(args);
        }

        protected override void RemoveListeners()
        {
            m_Properties.fightButton.onClick.RemoveListener(OnFightButtonClicked);
            m_Properties.bagButton.onClick.RemoveListener(OnBagButtonClicked);
            m_Properties.pokemonButton.onClick.RemoveListener(OnPokemonButtonClicked);
            m_Properties.runButton.onClick.RemoveListener(OnRunButtonClicked);
        }

 */ 