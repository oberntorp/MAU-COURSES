using ApusAnimalMotel.AnimalFactoryInformation;
using ApusAnimalMotel.Enums;
using ApusAnimalMotel.Enums.AnimalCommon;
using ApusAnimalMotel.Enums.Bird;
using ApusAnimalMotel.Enums.Dog;
using ApusAnimalMotel.Enums.Fish;
using ApusAnimalMotel.Enums.Horse;
using ApusAnimalMotel.Enums.TypeOfInput;
using ApusAnimalMotel.Mammals;
using ApusAnimalMotel.Subclasses.Birds;
using ApusAnimalMotel.Subclasses.Dogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApusAnimalMotel
{
    /// <summary>
    /// The Mainform of the application, the only form for now
    /// </summary>
    public partial class MainForm : Form
    {
        private string typeWithInSpeciesLabelDefault = string.Empty;
        private AnimalManager animalManager;
        private AnimalFactory animalFactory;

        /// <summary>
        /// The constructor of the MainForm, here I set the default Enabled and default values of different controls
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            animalManager = new AnimalManager();
            InitializeListBoxes();
            InitializeComboBoxes();
            typeWithInSpeciesLabelDefault = TypeWithinSpeciesLabel.Text;
            HideErrorsGroupBox();
            DisablePropertyGroupBox();
        }

        /// <summary>
        /// Here I add add values to different ListBoxes of the GUI
        /// </summary>
        private void InitializeListBoxes()
        {
            AnimalSpeciesListBox.Items.AddRange(Enum.GetNames(typeof(AnimalSpecies)));
        }

        /// <summary>
        /// Here I add add values to the comboboxes generic to animals
        /// </summary>
        private void InitializeComboBoxes()
        {
            GenderOfAnimalComboBox.Items.AddRange(Enum.GetNames(typeof(Gender)));
        }

        /// <summary>
        /// Hide the GroupBox that later on contains errors
        /// </summary>
        private void HideErrorsGroupBox()
        {
            ErrorsGroupBox.Enabled = false;
            ErrorsListBox.Enabled = false;
        }

        /// <summary>
        /// Disable PropertyGroupBox
        /// </summary>
        private void DisablePropertyGroupBox()
        {
            AnimalPropertiesGroupBox.Enabled = false;
        }

        /// <summary>
        /// Click event handler for adding an animal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addAnimalButton_Click(object sender, EventArgs e)
        {
            HideErrorsGroupBox();
            ClearErrorsListBox();
            BirdInformationForFactory birdInformation = new BirdInformationForFactory();
            FishInformationForFactory fishInformation = new FishInformationForFactory();
            DogInformationForFactory dogInformation = new DogInformationForFactory();
            HorseInformationForFactory horseInformation = new HorseInformationForFactory();

            string name = (string)CheckValidityOfInput(TypeOfInput.Name);
            int age = (int)CheckValidityOfInput(TypeOfInput.Age);
            Gender gender = (Gender)CheckValidityOfInput(TypeOfInput.Gender);
            AnimalSpecies animalSpecies = (AnimalSpecies)(int)CheckValidityOfInput(TypeOfInput.AnimalSpecies);
            int typeWithinSpecies = (int)CheckValidityOfInput(TypeOfInput.TypeWithinSpecies);
            int numberOfTeeth = (int)CheckValidityOfInput(TypeOfInput.Teeth);

            if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Bird)
            {
                CollectBirdInformation(birdInformation);
                animalFactory = new AnimalFactory(name, age, gender, animalSpecies, typeWithinSpecies, birdInformation);
            }

            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Fish)
            {
                CollectFishInformation(fishInformation);
                animalFactory = new AnimalFactory(name, age, gender, animalSpecies, typeWithinSpecies, fishInformation);
            }

            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Dog)
            {
                CollectDogInformation(dogInformation);
                animalFactory = new AnimalFactory(name, age, gender, animalSpecies, typeWithinSpecies, dogInformation, numberOfTeeth);

            }

            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Horse)
            {
                CollectHorseInformation(horseInformation);
                animalFactory = new AnimalFactory(name, age, gender, animalSpecies, typeWithinSpecies, horseInformation, numberOfTeeth);

            }

            Animal animalAfterSpecies = animalFactory.CreateAnimal(animalManager.GenerateIndexOfAnimal());

            // Since some values are not used if else than dog/horse, then I need to have the check before adding to the registry
            if (NoErrors())
            {
                animalManager.Add(animalAfterSpecies);
                FillAnimalsAddedListBox();
            }
            else
                SetErrorsGroupBoxEnabled();
        }

        /// <summary>
        /// Fill information about bird in object
        /// </summary>
        /// <param name="birdInformation">object to be filled</param>
        private void CollectBirdInformation(BirdInformationForFactory birdInformation)
        {
            birdInformation.BirdType = (BirdType)CheckValidityOfInput(TypeOfInput.BirdType);
            birdInformation.WhatToSing = (string)CheckValidityOfInput(TypeOfInput.WhatToSing);
            birdInformation.WhatSilverDoCrowsLike = (string)CheckValidityOfInput(TypeOfInput.WhatSilverDoCrowsLike);
            birdInformation.WingSpan = (int)CheckValidityOfInput(TypeOfInput.WingSpan);
            birdInformation.Plumage = (Plumage)CheckValidityOfInput(TypeOfInput.Plumage);
            birdInformation.TypeOfBeak = (BeakType)CheckValidityOfInput(TypeOfInput.TypeOfBeak);
        }

        /// <summary>
        /// Fill information about fish in object
        /// </summary>
        /// <param name="fishInformation">object to be filled</param>
        private void CollectFishInformation(FishInformationForFactory fishInformation)
        {
            fishInformation.IDOForget = (bool)CheckValidityOfInput(TypeOfInput.IsCosyDog);
            fishInformation.NumberOfFins = (int)CheckValidityOfInput(TypeOfInput.NumberOfFins);
            fishInformation.WhyIDangerous = (string)CheckValidityOfInput(TypeOfInput.WhyIDangerous);
            fishInformation.FishFamily = (FishFamily)CheckValidityOfInput(TypeOfInput.FishFamily);
            fishInformation.BeakType = (BeakType)CheckValidityOfInput(TypeOfInput.TypeOfBeak);
        }

        /// <summary>
        /// Fill information about dog in object
        /// </summary>
        /// <param name="dogInformation">object to be filled</param>
        private void CollectDogInformation(DogInformationForFactory dogInformation)
        {
            dogInformation.FurType = (string)CheckValidityOfInput(TypeOfInput.FurType);
            dogInformation.TeilLength = (int)CheckValidityOfInput(TypeOfInput.TeilLength);
            dogInformation.NumberOfLegs = (int)CheckValidityOfInput(TypeOfInput.NumerOfLegs);
            dogInformation.IsCosy = (bool)CheckValidityOfInput(TypeOfInput.IsCosyDog);
            dogInformation.UseCases = (AnimalUseCases)CheckValidityOfInput(TypeOfInput.AnimalUseCases);
            dogInformation.NumberOfChildren = (int)CheckValidityOfInput(TypeOfInput.numberOfChildrenLastBirth);
        }

        /// <summary>
        /// Fill information about horse in object
        /// </summary>
        /// <param name="horseInformation">object to be filled</param>
        private void CollectHorseInformation(HorseInformationForFactory horseInformation)
        {
            horseInformation.TeilLength = (int)CheckValidityOfInput(TypeOfInput.TeilLength);
            horseInformation.Withers = (int)CheckValidityOfInput(TypeOfInput.Withers);
            horseInformation.NumberOfLegs = (int)CheckValidityOfInput(TypeOfInput.NumerOfLegs);
            horseInformation.WhoLikesToRideBack = (string)CheckValidityOfInput(TypeOfInput.WhoLikesToRideBack);
            horseInformation.Sturdy = (bool)CheckValidityOfInput(TypeOfInput.Sturdy);
        }

        /// <summary>
        /// At click on addAnimals, clear the errors listbox
        /// </summary>
        private void ClearErrorsListBox()
        {
            ErrorsListBox.Items.Clear();
        }

        /// <summary>
        /// This is a method that performs different checks on the input
        /// </summary>
        /// <param name="typeOfInput">enum that is used by this method basicly to have the right error messages</param>
        /// <returns>Since this method can return different types I thought this could be a quick way to do it (not the best way, maybe)</returns>
        private object CheckValidityOfInput(TypeOfInput typeOfInput)
        {
            // Used to deside what ind of input to get
            bool isStringValue = ((typeOfInput == TypeOfInput.Name) || (typeOfInput == TypeOfInput.FurType) || (typeOfInput == TypeOfInput.WhatSilverDoCrowsLike) || (typeOfInput == TypeOfInput.WhatToSing) || (typeOfInput == TypeOfInput.WhoLikesToRideBack) || (typeOfInput == TypeOfInput.WhyIDangerous) || (typeOfInput == TypeOfInput.WhatToSing));
            bool isIntergerValue = ((typeOfInput == TypeOfInput.NumberOfFins) || (typeOfInput == TypeOfInput.NumerOfLegs) || (typeOfInput == TypeOfInput.Teeth) || (typeOfInput == TypeOfInput.TeilLength) || (typeOfInput == TypeOfInput.Age) || (typeOfInput == TypeOfInput.WingSpan) || (typeOfInput == TypeOfInput.Withers));
            bool isCheckValue = ((typeOfInput == TypeOfInput.IsCosyDog) || (typeOfInput == TypeOfInput.IDoForget) || (typeOfInput == TypeOfInput.Sturdy));
            bool isComboBoxValue = ((typeOfInput == TypeOfInput.Gender) || (typeOfInput == TypeOfInput.FishFamily) || (typeOfInput == TypeOfInput.AnimalUseCases) || (typeOfInput == TypeOfInput.TypeOfBeak) || (typeOfInput == TypeOfInput.BirdType) || (typeOfInput == TypeOfInput.Plumage));

            object returnValue;

            if (isStringValue)
            {
                if (ReadInputString(typeOfInput, out string result))
                {
                    returnValue = result;
                }
                else
                {
                    AddErrorsForStringInput(typeOfInput);
                    returnValue = "";
                }
            }
            else if (isIntergerValue)
            {
                if (ReadInputInteger(typeOfInput, out int result))
                {
                    returnValue = result;
                    if (!IsNumberGreaterThanZero(result))
                        ErrorsListBox.Items.Add("Check the numbers entered, they can not be 0");
                }
                else
                {
                    AddErrorsforIntegerInput(typeOfInput);
                    returnValue = -1;
                }
            }
            else if (isCheckValue)
            {
                if (IsCheckBoxChecked())
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }
            else if (isComboBoxValue)
            {
                if (IsComboBoxValueChosen(typeOfInput))
                {
                    returnValue = GetComboBoxValue(typeOfInput);
                }
                else
                {
                    AddErrorsForComboBoxInput(typeOfInput);
                    returnValue = -1;
                }
            }
            else if (typeOfInput == TypeOfInput.AnimalSpecies)
            {
                if (IsAnimalSpeciesSelected())
                    returnValue = GetSpeciesSelection();
                else
                {
                    ErrorsListBox.Items.Add("You must select a Species");
                    returnValue = GetSpeciesSelection();
                }
            }

            else
            {
                if (IsTypeWithinSpeciesSelected())
                    returnValue = GetTypeWithinSpeciesSelection();
                else
                {
                    ErrorsListBox.Items.Add("You must select a type within Species");
                    returnValue = GetTypeWithinSpeciesSelection();
                }
            }

            return returnValue;
        }

        private void AddErrorsforIntegerInput(TypeOfInput typeOfInput)
        {
            if (typeOfInput == TypeOfInput.Age)
            {
                ErrorsListBox.Items.Add("Please supply an age");
            }
            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Fish && typeOfInput == TypeOfInput.NumberOfFins)
            {
                ErrorsListBox.Items.Add("Please supply an number of fins");
            }
            else if (((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Dog || (AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Horse) && typeOfInput == TypeOfInput.TeilLength)
            {
                ErrorsListBox.Items.Add("Please supply an teil length");
            }
            else if (((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Dog || (AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Horse) && typeOfInput == TypeOfInput.Teeth)
            {
                ErrorsListBox.Items.Add("Please supply an number of teeth");
            }
            else if (((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Dog || (AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Horse) && typeOfInput == TypeOfInput.NumerOfLegs)
            {
                ErrorsListBox.Items.Add("Please supply an number of legs");
            }
            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Horse && typeOfInput == TypeOfInput.Withers)
            {
                ErrorsListBox.Items.Add("Please supply a withers");
            }
            else if ((BirdType)TypeWithinSpeciesListBox.SelectedIndex == BirdType.MarchSandpiper && typeOfInput == TypeOfInput.WingSpan)
            {
                ErrorsListBox.Items.Add("Please supply a wingspan");
            }
        }

        private bool ReadInputString(TypeOfInput typeOfInput, out string result)
        {
            result = string.Empty;
            if (typeOfInput == TypeOfInput.Name)
            {
                if (IsNameCurrectlyTyped())
                {
                    result = NameOfAnimalTextBox.Text;
                }
            }
            else if (typeOfInput == TypeOfInput.WhyIDangerous)
            {
                if (IsWhyIdDangerousCurrect())
                {
                    result = GenericStringProperty2OfAnimalTextBox.Text;
                }
            }
            else if (typeOfInput == TypeOfInput.WhoLikesToRideBack)
            {
                if (IsWhoLikesToRideBackTypedCurrectly())
                {
                    result = GenericStringProperty5OfAnimalTextBox.Text;
                }
            }
            else if (typeOfInput == TypeOfInput.FurType)
            {
                if (IsFurTypeTypedCurrectly())
                {
                    result = GenericStringProperty4OfAnimalTextBox.Text;
                }
            }
            else
            {
                if (IsOtherStringValuesCurrect())
                {
                    result = GenericStringProperty1OfAnimalTextBox.Text;
                }
            }

            return (result != string.Empty) ? true : false;
        }

        /// <summary>
        /// Checks that the name is entered
        /// </summary>
        /// <returns></returns>
        private bool IsNameCurrectlyTyped()
        {
            return NameOfAnimalTextBox.Text != string.Empty;
        }

        /// <summary>
        /// Checks that WhyIDangerous is entered
        /// </summary>
        /// <returns></returns>
        private bool IsWhyIdDangerousCurrect()
        {
            return GenericStringProperty2OfAnimalTextBox.Text != string.Empty;
        }

        /// <summary>
        /// Checks that values usinbg GenericStringProperty1OfAnimalTextBox is entered
        /// </summary>
        /// <returns></returns>
        private bool IsOtherStringValuesCurrect()
        {
            return GenericStringProperty1OfAnimalTextBox.Text != string.Empty;
        }

        /// <summary>
        /// Checks that howridespony is entered
        /// </summary>
        /// <returns></returns>
        private bool IsWhoLikesToRideBackTypedCurrectly()
        {
            return GenericStringProperty5OfAnimalTextBox.Text != string.Empty;
        }

        /// <summary>
        /// Checks that furtype is entered
        /// </summary>
        /// <returns></returns>
        private bool IsFurTypeTypedCurrectly()
        {
            return GenericStringProperty4OfAnimalTextBox.Text != string.Empty;
        }

        /// <summary>
        /// Adds errors for input gone wrong
        /// </summary>
        /// <param name="typeOfInput">what input to add error for (desides the message itself)</param>
        private void AddErrorsForStringInput(TypeOfInput typeOfInput)
        {
            if (typeOfInput == TypeOfInput.Name)
            {
                ErrorsListBox.Items.Add("Please supply a name");
            }
            else if ((DogType)TypeWithinSpeciesListBox.SelectedIndex == DogType.GoldenRetriever && typeOfInput == TypeOfInput.FurType)
            {
                ErrorsListBox.Items.Add("Please supply a fur type");
            }
            else if ((FishType)TypeWithinSpeciesListBox.SelectedIndex == FishType.Piraya && typeOfInput == TypeOfInput.WhyIDangerous)
            {
                ErrorsListBox.Items.Add("Please supply why I (piraya) is dangerous");
            }
            else if ((BirdType)TypeWithinSpeciesListBox.SelectedIndex == BirdType.Crow && typeOfInput == TypeOfInput.WhatSilverDoCrowsLike)
            {
                ErrorsListBox.Items.Add("Please supply what silver crows like");
            }
            else if ((BirdType)TypeWithinSpeciesListBox.SelectedIndex == BirdType.Bullfinch && typeOfInput == TypeOfInput.WhatToSing)
            {
                ErrorsListBox.Items.Add("Please supply what to sing");
            }
            else if ((HorseType)TypeWithinSpeciesListBox.SelectedIndex == HorseType.Pony && typeOfInput == TypeOfInput.WhoLikesToRideBack)
            {
                ErrorsListBox.Items.Add("Please supply who likes to ride the ponys back");
            }
        }

        /// <summary>
        /// CHecks that the checkbox is entered
        /// </summary>
        /// <returns></returns>
        private bool IsCheckBoxChecked()
        {
            return GenericCheckBox.Checked;
        }

        /// <summary>
        /// Checks that a value in a combobox is set
        /// </summary>
        /// <param name="typeOfInput">what value to check</param>
        /// <returns></returns>
        private bool IsComboBoxValueChosen(TypeOfInput typeOfInput)
        {

            if (typeOfInput == TypeOfInput.Gender)
            {
                return GenderOfAnimalComboBox.SelectedIndex != -1;
            }
            else if (typeOfInput == TypeOfInput.AnimalUseCases || typeOfInput == TypeOfInput.BirdType || typeOfInput == TypeOfInput.FishFamily)
            {
                return GenericComboBox1.SelectedIndex != -1;
            }
            else
            {
                return GenericComboBox2.SelectedIndex != -1;
            }
        }

        /// <summary>
        /// Is combobox value is not chosen, error is added here
        /// </summary>
        /// <param name="typeOfInput">what input to add error for (desides the message itself)</param>
        private void AddErrorsForComboBoxInput(TypeOfInput typeOfInput)
        {
            if (((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Fish) && (typeOfInput == TypeOfInput.FishFamily && GenericComboBox1.SelectedIndex == -1))
            {
                ErrorsListBox.Items.Add("Please supply an FishFamily");
            }
            else if ((((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Dog && (DogType)TypeWithinSpeciesListBox.SelectedIndex == DogType.Scheafer)) && (typeOfInput == TypeOfInput.AnimalUseCases && GenericComboBox1.SelectedIndex == -1))
            {
                ErrorsListBox.Items.Add("Please supply a use case for the Sheafer");
            }
            else if (((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Bird) && (typeOfInput == TypeOfInput.BirdType && GenericComboBox1.SelectedIndex == -1))
            {
                ErrorsListBox.Items.Add("Please supply a bird type");
            }
            else if (typeOfInput == TypeOfInput.Gender && GenderOfAnimalComboBox.SelectedIndex == -1)
            {
                ErrorsListBox.Items.Add("Please supply a gender");
            }
            else if (((BirdType)TypeWithinSpeciesListBox.SelectedIndex == BirdType.MarchSandpiper) && (typeOfInput == TypeOfInput.Plumage && GenericComboBox2.SelectedIndex == -1))
            {
                ErrorsListBox.Items.Add("Please supply a plumage");
            }
            else if (((BirdType)TypeWithinSpeciesListBox.SelectedIndex == BirdType.Woodpecker) && (typeOfInput == TypeOfInput.TypeOfBeak && GenericComboBox2.SelectedIndex == -1))
            {
                ErrorsListBox.Items.Add("Please supply a beak type");
            }
        }

        /// <summary>
        /// Get combobox value
        /// </summary>
        /// <param name="typeOfInput"></param>
        /// <returns></returns>
        private int GetComboBoxValue(TypeOfInput typeOfInput)
        {
            if (typeOfInput == TypeOfInput.Gender)
            {
                return GenderOfAnimalComboBox.SelectedIndex;
            }
            else if (typeOfInput == TypeOfInput.AnimalUseCases || typeOfInput == TypeOfInput.BirdType || typeOfInput == TypeOfInput.FishFamily)
            {
                return GenericComboBox1.SelectedIndex;
            }
            else
            {
                return GenericComboBox2.SelectedIndex;
            }

        }

        /// <summary>
        /// Reads integer input
        /// </summary>
        /// <param name="typeOfInput">what input to read</param>
        /// <param name="intResult">result variable holding the result</param>
        /// <returns></returns>
        private bool ReadInputInteger(TypeOfInput typeOfInput, out int intResult)
        {
            intResult = -1;
            if (typeOfInput == TypeOfInput.Age)
            {
                return int.TryParse(AgeOfAnimalTextBox.Text, out intResult);
            }
            else if (IntgersInputMakeUseOfFirstGenericTextBox(typeOfInput))
            {
                return int.TryParse(GenericStringProperty1OfAnimalTextBox.Text, out intResult);
            }
            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Horse && typeOfInput == TypeOfInput.TeilLength)
            {
                return int.TryParse(GenericStringProperty2OfAnimalTextBox.Text, out intResult);
            }
            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Horse && typeOfInput == TypeOfInput.Withers)
            {
                return int.TryParse(GenericStringProperty3OfAnimalTextBox.Text, out intResult);
            }
            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Horse && typeOfInput == TypeOfInput.NumerOfLegs)
            {
                return int.TryParse(GenericStringProperty4OfAnimalTextBox.Text, out intResult);
            }
            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Dog && typeOfInput == TypeOfInput.NumerOfLegs)
            {
                return int.TryParse(GenericStringProperty2OfAnimalTextBox.Text, out intResult);
            }
            else if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Dog && typeOfInput == TypeOfInput.TeilLength)
            {
                return int.TryParse(GenericStringProperty3OfAnimalTextBox.Text, out intResult);
            }
            else
            {
                if ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Dog)
                {
                    return int.TryParse(GenericStringProperty4OfAnimalTextBox.Text, out intResult);
                }

                return false;
            }
        }

        /// <summary>
        /// Checks what input integer uses the first input
        /// </summary>
        /// <param name="typeOfInput"></param>
        /// <returns></returns>
        private bool IntgersInputMakeUseOfFirstGenericTextBox(TypeOfInput typeOfInput)
        {
            return ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex == AnimalSpecies.Fish && typeOfInput == TypeOfInput.NumberOfFins) || (typeOfInput == TypeOfInput.Teeth) || ((BirdType)TypeWithinSpeciesListBox.SelectedIndex == BirdType.MarchSandpiper && typeOfInput == TypeOfInput.WingSpan);
        }

        /// <summary>
        /// Checks if the user has selected an animal species
        /// </summary>
        /// <returns>true/false</returns>
        private bool IsAnimalSpeciesSelected()
        {
            return AnimalSpeciesListBox.SelectedIndex > -1;
        }

        /// <summary>
        /// If an animal species was selected the selection is fetched here
        /// </summary>
        /// <returns>the selection</returns>
        private int GetSpeciesSelection()
        {
            return AnimalSpeciesListBox.SelectedIndex;
        }

        /// <summary>
        /// Used by CheckValidityOfInput to see if the number is greater than zero
        /// </summary>
        /// <param name="numberToCheck">the number to check</param>
        /// <returns></returns>
        private bool IsNumberGreaterThanZero(int numberToCheck)
        {
            return numberToCheck > 0;
        }

        /// <summary>
        /// Checks if the user has selected any type within a species
        /// </summary>
        /// <returns>true/false</returns>
        private bool IsTypeWithinSpeciesSelected()
        {
            return TypeWithinSpeciesListBox.SelectedIndex > -1;
        }

        /// <summary>
        /// If an type within an animal species was selected the selection is fetched here
        /// </summary>
        /// <returns>the selection</returns>
        private int GetTypeWithinSpeciesSelection()
        {
            return TypeWithinSpeciesListBox.SelectedIndex;
        }

        /// <summary>
        /// Used by the added animals click handler after all checks on input are performed
        /// </summary>
        /// <returns>true/false</returns>
        private bool NoErrors()
        {
            return ErrorsListBox.Items.Count == 0;
        }

        /// <summary>
        /// If everything with checks of input and creation of an animal was successfull, they are added by this method
        /// </summary>
        private void FillAnimalsAddedListBox()
        {
            ClearAnimalsAddedListBox();
            for (int i = 0; i < animalManager.ElementsOfAnimalList; i++)
            {
                AnimlsAddedListBox.Items.Add($"{animalFactory.ConvertAnimalToRightType(animalManager.GetAnimalAt(i))}");
            }
        }

        /// <summary>
        /// Clears the list of added animals
        /// </summary>
        private void ClearAnimalsAddedListBox()
        {
            AnimlsAddedListBox.Items.Clear();
        }

        /// <summary>
        /// If any errors are present, the errors are made Enabled here
        /// </summary>
        private void SetErrorsGroupBoxEnabled()
        {
            ErrorsGroupBox.Enabled = true;
            ErrorsListBox.Enabled = true;
        }

        /// <summary>
        /// This is the event handler of the listbox containing the animalSpecies
        /// It does Disable and enable different listBoxes depending on selection
        /// It changes the label of the typeWithIn species
        /// It fills the typeWithinSpecies listbox
        /// </summary>
        /// <param name="sender">the sending object</param>
        /// <param name="e">the event itself</param>
        private void AnimalSpeciesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComplementLabelTypeWithinSsecies();
            FillTypeWhithinSpeciesListBox();
            ClearGenericComboBoxes();
            EnablePropertyGroupBox();
            EnablePropertyInputBasedOnAnimalSpecies();
        }

        /// <summary>
        /// Enable specific input fields for properties based on selected animal
        /// </summary>
        private void EnablePropertyInputBasedOnAnimalSpecies()
        {
            DisableOther();
            switch ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex)
            {
                case AnimalSpecies.Bird:
                    EnableInputForBird();
                    break;
                case AnimalSpecies.Dog:
                    EnableMammalInput();
                    EnableInputForDog();
                    break;
                case AnimalSpecies.Fish:
                    EnableInputForFish();
                    break;
                case AnimalSpecies.Horse:
                    EnableMammalInput();
                    EnableInputForHorse();
                    break;
            }
        }

        /// <summary>
        /// Disable inputs not necessary for specific species
        /// </summary>
        private void DisableOther()
        {
            switch ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex)
            {
                case AnimalSpecies.Bird:
                    GenericStringProperty1OfAnimalLabel.Enabled = false;
                    GenericStringProperty1OfAnimalTextBox.Enabled = false;
                    GenericStringProperty2OfAnimalLabel.Enabled = false;
                    GenericStringProperty2OfAnimalTextBox.Enabled = false;
                    GenericStringProperty3OfAnimalLabel.Enabled = false;
                    GenericStringProperty3OfAnimalTextBox.Enabled = false;
                    GenericStringProperty4OfAnimalLabel.Enabled = false;
                    GenericStringProperty4OfAnimalTextBox.Enabled = false;
                    GenericStringProperty5OfAnimalLabel.Enabled = false;
                    GenericStringProperty5OfAnimalTextBox.Enabled = false;
                    GenericComboBox2Label.Enabled = false;
                    GenericComboBox2.Enabled = false;
                    GenericCheckBox.Enabled = false;
                    break;
                case AnimalSpecies.Dog:
                    GenericStringProperty2OfAnimalLabel.Enabled = false;
                    GenericStringProperty2OfAnimalTextBox.Enabled = false;
                    GenericStringProperty4OfAnimalLabel.Enabled = false;
                    GenericStringProperty4OfAnimalTextBox.Enabled = false;
                    GenericStringProperty5OfAnimalLabel.Enabled = false;
                    GenericStringProperty5OfAnimalTextBox.Enabled = false;
                    GenericComboBox1Label.Enabled = false;
                    GenericComboBox1.Enabled = false;
                    GenericComboBox2Label.Enabled = false;
                    GenericComboBox2.Enabled = false;
                    break;
                case AnimalSpecies.Fish:
                    GenericStringProperty3OfAnimalLabel.Enabled = false;
                    GenericStringProperty3OfAnimalTextBox.Enabled = false;
                    GenericStringProperty4OfAnimalLabel.Enabled = false;
                    GenericStringProperty4OfAnimalTextBox.Enabled = false;
                    GenericStringProperty5OfAnimalLabel.Enabled = false;
                    GenericStringProperty5OfAnimalTextBox.Enabled = false;
                    GenericComboBox2Label.Enabled = false;
                    GenericComboBox2.Enabled = false;
                    break;
                case AnimalSpecies.Horse:
                    GenericStringProperty4OfAnimalLabel.Enabled = false;
                    GenericStringProperty4OfAnimalTextBox.Enabled = false;
                    GenericComboBox1Label.Enabled = false;
                    GenericComboBox1.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// Enable specific input fields for the species bird
        /// </summary>
        private void EnableInputForBird()
        {
            FillComboBoxBirdType();

            GenericComboBox1Label.Text = "Type of bird";
            GenericComboBox1Label.Enabled = true;
            GenericComboBox1.Enabled = true;
        }
        
        /// <summary>
        /// Adds birdtype to first comboBox
        /// </summary>
        private void FillComboBoxBirdType()
        {
            GenericComboBox1.Items.AddRange(Enum.GetNames(typeof(BirdType)));
        }

        /// <summary>
        /// Enable input fields for dog species
        /// </summary>
        private void EnableInputForDog()
        {
            GenericStringProperty2OfAnimalLabel.Text = "Number of legs";
            GenericStringProperty2OfAnimalLabel.Enabled = true;
            GenericStringProperty2OfAnimalTextBox.Enabled = true;
            GenericStringProperty2OfAnimalTextBox.Text = "";

            GenericStringProperty3OfAnimalLabel.Text = "Teil length";
            GenericStringProperty3OfAnimalLabel.Enabled = true;
            GenericStringProperty3OfAnimalTextBox.Enabled = true;
            GenericStringProperty3OfAnimalTextBox.Text = "";
        }

        /// <summary>
        /// Enable input fields for fish species
        /// </summary>
        private void EnableInputForFish()
        {
            FillFishFamily();

            GenericStringProperty1OfAnimalLabel.Text = "Number of fins";
            GenericStringProperty1OfAnimalLabel.Enabled = true;
            GenericStringProperty1OfAnimalTextBox.Enabled = true;
            GenericStringProperty1OfAnimalTextBox.Text = "";

            GenericComboBox1Label.Text = "Fish Family";
            GenericComboBox1Label.Enabled = true;
            GenericComboBox1.Enabled = true;
        }

        /// <summary>
        /// Adds fish family to the first comboBox
        /// </summary>
        private void FillFishFamily()
        {
            GenericComboBox1.Items.AddRange(Enum.GetNames(typeof(FishFamily)));
        }

        /// <summary>
        /// Enable input fields for horse species
        /// </summary>
        private void EnableInputForHorse()
        {
            GenericStringProperty2OfAnimalLabel.Text = "Teil length";
            GenericStringProperty2OfAnimalLabel.Enabled = true;
            GenericStringProperty2OfAnimalTextBox.Enabled = true;
            GenericStringProperty2OfAnimalTextBox.Text = "";

            GenericStringProperty3OfAnimalLabel.Text = "Withers";
            GenericStringProperty3OfAnimalLabel.Enabled = true;
            GenericStringProperty3OfAnimalTextBox.Enabled = true;
            GenericStringProperty3OfAnimalTextBox.Text = "";

            GenericStringProperty4OfAnimalLabel.Text = "NumberOfLegs";
            GenericStringProperty4OfAnimalLabel.Enabled = true;
            GenericStringProperty4OfAnimalTextBox.Enabled = true;
            GenericStringProperty4OfAnimalTextBox.Text = "";
        }

        /// <summary>
        /// If changed I do first restore to the initialized value of the labels text
        /// The label above TypeWithinSpeciesListBox is there amended with the animal species if one is selected
        /// </summary>
        private void ComplementLabelTypeWithinSsecies()
        {
            RestoreTypeWithinSpeciesLabelIfChanged();
            TypeWithinSpeciesLabel.Text += $" {AnimalSpeciesListBox.SelectedItem.ToString()}";
        }

        /// <summary>
        /// Restores the label of the typeWithinnspecies listbox
        /// </summary>
        private void RestoreTypeWithinSpeciesLabelIfChanged()
        {
            if (TypeWithinSpeciesLabel.Text != typeWithInSpeciesLabelDefault)
                TypeWithinSpeciesLabel.Text = typeWithInSpeciesLabelDefault;
        }

        /// <summary>
        /// Clears the listbox for typesWithinSpecies
        /// Fills the listbox for typesWithinSpecies when an animal species is selected
        /// </summary>
        private void FillTypeWhithinSpeciesListBox()
        {
            ClearTypeWhithinSpeciesListBox();
            switch ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex)
            {
                case AnimalSpecies.Dog:
                    TypeWithinSpeciesListBox.Items.AddRange(Enum.GetNames(typeof(DogType)));
                    break;
                case AnimalSpecies.Bird:
                    TypeWithinSpeciesListBox.Items.AddRange(Enum.GetNames(typeof(BirdType)));
                    break;
                case AnimalSpecies.Fish:
                    TypeWithinSpeciesListBox.Items.AddRange(Enum.GetNames(typeof(FishType)));
                    break;
                case AnimalSpecies.Horse:
                    TypeWithinSpeciesListBox.Items.AddRange(Enum.GetNames(typeof(HorseType)));
                    break;

            }
        }

        /// <summary>
        /// clears the listbox for typesWithinSpecies
        /// </summary>
        private void ClearTypeWhithinSpeciesListBox()
        {
            TypeWithinSpeciesListBox.Items.Clear();
        }

        /// <summary>
        /// Event handler for the ClearAnimalsAdded button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearAnimalsAddedButton_Click(object sender, EventArgs e)
        {
            AnimlsAddedListBox.Items.Clear();
        }

        /// <summary>
        /// Event handler for the RemoveSelectedAnimalButton button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveSelectedAnimalButton_Click(object sender, EventArgs e)
        {
            animalManager.RemoveAnimalAt(AnimlsAddedListBox.SelectedIndex);
            FillAnimalsAddedListBox();
        }

        /// <summary>
        /// Event handler for click event when clicking a type within species
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TypeWithinSpeciesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((AnimalSpecies)AnimalSpeciesListBox.SelectedIndex)
            {
                case AnimalSpecies.Dog:
                    EnableInputPerDogType();
                    break;
                case AnimalSpecies.Bird:
                    EnableInputPerBirdType();
                    break;
                case AnimalSpecies.Fish:
                    EnableInputPerFishType();
                    break;
                case AnimalSpecies.Horse:
                    EnableInputPerHorseType();
                    break;
            }
        }

        /// <summary>
        /// Clear ComboBoxes when selecting another typeWithinSpecies
        /// </summary>
        private void ClearGenericComboBoxes()
        {
            GenericComboBox1.Items.Clear();
            GenericComboBox1.Text = "";
            GenericComboBox2.Items.Clear();
            GenericComboBox2.Text = "";
        }

        /// <summary>
        /// Enable PropertyGroupBox
        /// </summary>
        private void EnablePropertyGroupBox()
        {
            AnimalPropertiesGroupBox.Enabled = true;
        }

        /// <summary>
        /// Set label and textbox for Mammal input as Enabled
        /// </summary>
        private void EnableMammalInput()
        {
            GenericStringProperty1OfAnimalLabel.Text = "Number of teeth";
            GenericStringProperty1OfAnimalLabel.Enabled = true;

            GenericStringProperty1OfAnimalTextBox.Enabled = true;
            GenericStringProperty1OfAnimalTextBox.Text = "";
        }

        /// <summary>
        /// Enable/Disable the inputs required/not required for diffrent dogs
        /// </summary>
        private void EnableInputPerDogType()
        {
            if ((DogType)TypeWithinSpeciesListBox.SelectedIndex == DogType.GoldenRetriever)
            {
                DisableInputNotForGoldenRetriever();
                EnableInputForGoldenRetriever();
            }
            else if ((DogType)TypeWithinSpeciesListBox.SelectedIndex == DogType.Poodle)
            {
                DisableInputNotForPoodle();
                EnableInputForPoodle();
            }
            else
            {
                DisableInputNotForScheafer();
                EnableInputForScheafer();
            }
        }

        /// <summary>
        /// Disable inputs not required for GoldenRetriever
        /// </summary>
        private void DisableInputNotForGoldenRetriever()
        {
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalTextBox.Enabled = false;
            GenericCheckBox.Enabled = false;
            GenericComboBox1Label.Enabled = true;
            GenericComboBox1.Enabled = false;
            GenericComboBox2Label.Enabled = false;
            GenericComboBox2.Enabled = false;
        }

        /// <summary>
        /// Enable inputs required for GoldenRetriever
        /// </summary>
        private void EnableInputForGoldenRetriever()
        {
            GenericStringProperty4OfAnimalLabel.Text = "Fur type";
            GenericStringProperty4OfAnimalTextBox.Text = "";

            GenericStringProperty4OfAnimalLabel.Enabled = true;
            GenericStringProperty4OfAnimalTextBox.Enabled = true;
        }

        /// <summary>
        /// Disable inputs not required for Poodle
        /// </summary>
        private void DisableInputNotForPoodle()
        {
            GenericStringProperty4OfAnimalLabel.Enabled = false;
            GenericStringProperty4OfAnimalTextBox.Enabled = false;
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalTextBox.Enabled = false;
            GenericComboBox1Label.Enabled = false;
            GenericComboBox1.Enabled = false;
            GenericComboBox2Label.Enabled = false;
            GenericComboBox2.Enabled = false;
        }

        /// <summary>
        /// Enable inputs required for poodle
        /// </summary>
        private void EnableInputForPoodle()
        {
            GenericCheckBox.Text = "Is the dog cosy?";
            GenericCheckBox.Enabled = true;
        }

        /// <summary>
        /// Disable inputs not required by Scheafer
        /// </summary>
        private void DisableInputNotForScheafer()
        {
            GenericStringProperty4OfAnimalLabel.Enabled = false;
            GenericStringProperty4OfAnimalTextBox.Enabled = false;
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalTextBox.Enabled = false;
            GenericCheckBox.Enabled = false;
            GenericComboBox2Label.Enabled = false;
            GenericComboBox2.Enabled = false;
        }

        /// <summary>
        /// Enable inputs required for Scheafer
        /// </summary>
        private void EnableInputForScheafer()
        {
            GenericComboBox1Label.Text = "Use case";
            GenericComboBox1Label.Enabled = true;
            GenericComboBox1.Items.AddRange(Enum.GetNames(typeof(AnimalUseCases)));
            GenericComboBox1.Enabled = true;
        }

        /// <summary>
        /// Enable the inputs required for diffrent birds
        /// </summary>
        private void EnableInputPerBirdType()
        {
            if ((BirdType)TypeWithinSpeciesListBox.SelectedIndex == BirdType.Bullfinch)
            {
                DisableInputNotForBullfinch();
                EnableInputForBullfinch();
            }
            else if ((BirdType)TypeWithinSpeciesListBox.SelectedIndex == BirdType.Crow)
            {
                DisableInputNotForCrow();
                EnableInputForCrow();
            }
            else if ((BirdType)TypeWithinSpeciesListBox.SelectedIndex == BirdType.MarchSandpiper)
            {
                DisableInputNotForMarchSandpiper();
                EnableInputForMarchSandpiper();
            }
            else
            {
                DisableInputNotForWoodpecker();
                EnableInputForWoodPecker();
            }
        }

        /// <summary>
        /// Disable inputs not required for BullFinch
        /// </summary>
        private void DisableInputNotForBullfinch()
        {
            GenericStringProperty2OfAnimalTextBox.Enabled = false;
            GenericStringProperty2OfAnimalLabel.Enabled = false;
            GenericStringProperty3OfAnimalTextBox.Enabled = false;
            GenericStringProperty3OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalTextBox.Enabled = false;
            GenericCheckBox.Enabled = false;
            GenericComboBox2Label.Enabled = false;
            GenericComboBox2.Enabled = false;
        }

        /// <summary>
        /// Enable the inputs required for bullfinch
        /// </summary>
        private void EnableInputForBullfinch()
        {
            GenericStringProperty1OfAnimalLabel.Text = "What to sing";
            GenericStringProperty1OfAnimalLabel.Enabled = true;
            GenericStringProperty1OfAnimalTextBox.Text = "";
            GenericStringProperty1OfAnimalTextBox.Enabled = true;
        }

        /// <summary>
        /// Disable inputs not required for Crow
        /// </summary>
        private void DisableInputNotForCrow()
        {
            GenericStringProperty2OfAnimalLabel.Enabled = false;
            GenericStringProperty2OfAnimalTextBox.Enabled = false;
            GenericStringProperty3OfAnimalLabel.Enabled = false;
            GenericStringProperty3OfAnimalTextBox.Enabled = false;
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty2OfAnimalTextBox.Enabled = false;
            GenericCheckBox.Enabled = false;
            GenericComboBox2Label.Enabled = false;
            GenericComboBox2.Enabled = false;

        }

        /// <summary>
        /// Enable the inputs required for crow
        /// </summary>
        private void EnableInputForCrow()
        {
            GenericStringProperty1OfAnimalLabel.Text = "What silver do the crow like";
            GenericStringProperty1OfAnimalLabel.Enabled = true;
            GenericStringProperty1OfAnimalTextBox.Text = "";
            GenericStringProperty1OfAnimalTextBox.Enabled = true;
        }

        /// <summary>
        /// Disable inputs not required for MarchSandpiper
        /// </summary>
        private void DisableInputNotForMarchSandpiper()
        {
            GenericStringProperty2OfAnimalLabel.Enabled = false;
            GenericStringProperty2OfAnimalTextBox.Enabled = false;
            GenericStringProperty3OfAnimalLabel.Enabled = false;
            GenericStringProperty3OfAnimalTextBox.Enabled = false;
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalTextBox.Enabled = false;
            GenericCheckBox.Enabled = false;
        }

        /// <summary>
        /// Enable the inputs required for masrch sandpiper
        /// </summary>
        private void EnableInputForMarchSandpiper()
        {
            GenericStringProperty1OfAnimalLabel.Text = "Wingspan";
            GenericStringProperty1OfAnimalLabel.Enabled = true;
            GenericStringProperty1OfAnimalTextBox.Text = "";
            GenericStringProperty1OfAnimalTextBox.Enabled = true;

            GenericComboBox2Label.Text = "Plumage";
            GenericComboBox2Label.Enabled = true;
            FillComboBoxPlumage();
            GenericComboBox2.Enabled = true;
        }

        /// <summary>
        /// Fill plumage values into combobox
        /// </summary>
        private void FillComboBoxPlumage()
        {
            GenericComboBox2.Items.Clear();
            GenericComboBox2.Items.AddRange(Enum.GetNames(typeof(Plumage)));
        }

        /// <summary>
        /// Disable inputs not required for Woodpecker
        /// </summary>
        private void DisableInputNotForWoodpecker()
        {
            GenericStringProperty1OfAnimalLabel.Enabled = false;
            GenericStringProperty1OfAnimalTextBox.Enabled = false;
            GenericStringProperty2OfAnimalLabel.Enabled = false;
            GenericStringProperty2OfAnimalTextBox.Enabled = false;
            GenericStringProperty3OfAnimalLabel.Enabled = false;
            GenericStringProperty3OfAnimalTextBox.Enabled = false;
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalTextBox.Enabled = false;
            GenericCheckBox.Enabled = false;

        }

        /// <summary>
        /// Enable the inputs required for woodpecker
        /// </summary>
        private void EnableInputForWoodPecker()
        {
            GenericComboBox2Label.Text = "Type of beak";
            GenericComboBox2Label.Enabled = true;
            GenericComboBox2.Enabled = true;
            FillComboBoxBeakType();
        }

        /// <summary>
        /// Fill beaktype values into combobox
        /// </summary>
        private void FillComboBoxBeakType()
        {
            GenericComboBox2.Items.Clear();
            GenericComboBox2.Items.AddRange(Enum.GetNames(typeof(BeakType)));
        }

        /// <summary>
        /// Enable/Disable the inputs required/not required for diffrent fiches
        /// </summary>
        private void EnableInputPerFishType()
        {
            if ((FishType)TypeWithinSpeciesListBox.SelectedIndex == FishType.GoldFish)
            {
                DisableInputNotForGoldFish();
                EnableInputForGoldFish();
            }
            else
            {
                DisableInputNotForPiraya();
                EnableInputForPiraya();
            }
        }

        /// <summary>
        /// Enable the inputs not required for GoldFish
        /// </summary>
        private void DisableInputNotForGoldFish()
        {
            GenericStringProperty2OfAnimalLabel.Enabled = false;
            GenericStringProperty2OfAnimalTextBox.Enabled = false;
            GenericStringProperty3OfAnimalLabel.Enabled = false;
            GenericStringProperty3OfAnimalTextBox.Enabled = false;
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericComboBox2Label.Enabled = false;
            GenericComboBox2.Enabled = false;
        }

        /// <summary>
        /// Enable the inputs required for GoldFish
        /// </summary>
        private void EnableInputForGoldFish()
        {
            GenericCheckBox.Text = "Bad memory";
            GenericCheckBox.Enabled = true;
        }

        /// <summary>
        /// Disable the inputs not required for Piraya
        /// </summary>
        private void DisableInputNotForPiraya()
        {
            GenericStringProperty3OfAnimalLabel.Enabled = false;
            GenericStringProperty3OfAnimalTextBox.Enabled = false;
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalTextBox.Enabled = false;
            GenericComboBox2Label.Enabled = false;
            GenericComboBox2.Enabled = false;
            GenericCheckBox.Enabled = false;
        }

        /// <summary>
        /// Enable the inputs required for Piraya
        /// </summary>
        private void EnableInputForPiraya()
        {
            GenericStringProperty2OfAnimalLabel.Text = "Why am I dangerous";
            GenericStringProperty2OfAnimalLabel.Enabled = true;
            GenericStringProperty2OfAnimalTextBox.Text = "";
            GenericStringProperty2OfAnimalTextBox.Enabled = true;
        }

        /// <summary>
        /// Enable/Disable the inputs required/not required for diffrent horses
        /// </summary>
        private void EnableInputPerHorseType()
        {
            if ((HorseType)TypeWithinSpeciesListBox.SelectedIndex == HorseType.Pony)
            {
                DisableInputNotForPony();
                EnableInputForPony();
            }
            else
            {
                DisableInputNotForTarpan();
                EnableInputForTarpan();
            }
        }

        /// <summary>
        /// Disable the inputs not required for Pony
        /// </summary>
        private void DisableInputNotForPony()
        {
            GenericComboBox1Label.Enabled = false;
            GenericComboBox1.Enabled = false;
            GenericComboBox2Label.Enabled = false;
            GenericComboBox2.Enabled = false;
            GenericCheckBox.Enabled = false;

        }

        /// <summary>
        /// Enable the inputs required for Pony
        /// </summary>
        private void EnableInputForPony()
        {
            GenericStringProperty5OfAnimalLabel.Text = "Who likes to ride pony´s back";
            GenericStringProperty5OfAnimalLabel.Enabled = true;
            GenericStringProperty5OfAnimalTextBox.Text = "";
            GenericStringProperty5OfAnimalTextBox.Enabled = true;
        }

        /// <summary>
        /// Disable the inputs not required for Tarpan
        /// </summary>
        private void DisableInputNotForTarpan()
        {
            GenericStringProperty5OfAnimalLabel.Enabled = false;
            GenericStringProperty5OfAnimalTextBox.Enabled = false;
            GenericComboBox1Label.Enabled = false;
            GenericComboBox1.Enabled = false;
            GenericComboBox2Label.Enabled = false;
            GenericComboBox2.Enabled = false;
        }

        /// <summary>
        /// Enable the inputs required for Tarpan
        /// </summary>
        private void EnableInputForTarpan()
        {
            GenericCheckBox.Text = "Is the horse sturdy";
            GenericCheckBox.Enabled = true;
        }

        /// <summary>
        /// Evend handler when clicking an added animal
        /// </summary>
        /// <param name="sender">the gui component fiering the event</param>
        /// <param name="e">The event arguments</param>
        private void AnimlsAddedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearFoodInformation();
            EnterFoodInformation();
        }

        /// <summary>
        /// Clears FoodScheduleListBox of its items.
        /// </summary>
        private void ClearFoodInformation()
        {
            FoodScheduleListBox.Items.Clear();
        }

        /// <summary>
        /// Method to populate the information within the foodScheduleGroupBox
        /// </summary>
        private void EnterFoodInformation()
        {
            Animal animalToGetInformationOf = animalManager.GetAnimalAt(AnimlsAddedListBox.SelectedIndex);

            SetEaterTypeInGUI(animalToGetInformationOf);
            OutputFoodScheduleToGUI(animalToGetInformationOf);
        }

        /// <summary>
        /// Set the Eater type of an animal to the textbox containing this info
        /// </summary>
        /// <param name="animal">the animal to get the food information from</param>
        private void SetEaterTypeInGUI(Animal animal)
        {
            EaterTextBox.Text = animal.GetEaterType().ToString();
        }

        /// <summary>
        /// Gets the food schedule for a animal species printed to the GUI
        /// </summary>
        /// <param name="animal">animal that has foodSchedule to output</param>
        private void OutputFoodScheduleToGUI(Animal animal)
        {
            FoodScheduleListBox.Items.AddRange(animal.GetFoodSchedule().GetFoodScheduleAllItems().ToArray());
        }
    }
}
