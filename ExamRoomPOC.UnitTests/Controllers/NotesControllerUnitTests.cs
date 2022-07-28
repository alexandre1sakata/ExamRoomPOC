using ExamRoomPOC.API.Controllers;
using ExamRoomPOC.Domain.Interfaces.Services;
using ExamRoomPOC.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ExamRoomPOC.UnitTests.Controllers
{
    public class NotesControllerUnitTests
    {
        private readonly INotesService _notesService;
        public NotesController Controller { get; set; }

        public NotesControllerUnitTests()
        {
            _notesService = Substitute.For<INotesService>();
            Controller = new NotesController(_notesService);
        }

        #region GET ALL

        [Fact]
        public void GetAllNotesWithSuccess()
        {
            _notesService.GetAll()
                .Returns(new List<Note>
                {
                    new Note() { Id = 1, Title = "Title_test_01", Description = "Desc_test_01" },
                    new Note() { Id = 2, Title = "Title_test_02", Description = "Desc_test_02" }
                });

            var resultController = Controller.GetAllNotes();

            var result = resultController.Result.Result;

            result.Should().NotBeNull();
        }

        [Fact]
        public void GetAllNotesWithResultNotFound()
        {
            _notesService.GetAll().Returns(new List<Note> { });

            var result = Controller.GetAllNotes();

            result.Should().NotBeNull();
        }

        #endregion

        #region GET BY ID

        [Fact]
        public void GetNoteByIdWithSuccess()
        {
            _notesService.GetById(Arg.Any<int>())
               .Returns(new Note()
               {
                   Id = 1,
                   Title = "Title_test_01",
                   Description = "Desc_test_01"
               });

            var resultController = Controller.GetNoteById(1);

            var result = resultController.Result;

            result.Should().NotBeNull();
        }

        [Fact]
        public void GetNoteByIdWithNotFound()
        {
            _notesService.GetById(Arg.Any<int>()).Returns(new Note());

            var resultController = Controller.GetNoteById(1);

            var result = resultController.Result;

            result.Should().NotBeNull();
        }

        #endregion

        [Fact]
        public void CreateNoteWithSuccess()
        {
            _notesService.Create(Arg.Any<Note>());

            var resultController = Controller.CreateNote(new Note 
            { 
                Title = "Title_test_01", Description = "Desc_test_01" 
            });

            var result = resultController;

            result.Should().NotBeNull();
        }

        [Fact]
        public void ModifyNoteWithSuccess()
        {
            _notesService.Modify(Arg.Any<Note>(), Arg.Any<int>());

            var resultController = Controller.ModifyNote(new Note
            {
                Title = "Title_test_01_Updated",
                Description = "Desc_test_01_Updated"
            }, 1);

            var result = resultController;

            result.Should().NotBeNull();
        }

        [Fact]
        public void ExcludeNoteWithSuccess()
        {
            _notesService.Exclude(Arg.Any<int>());

            var resultController = Controller.ExcludeNote(1);

            var result = resultController;

            result.Should().NotBeNull();
        }
    }
}
